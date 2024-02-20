using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Handles
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(VerifyEmailRequest request, CancellationToken cancellationToken)
        {
            var data = request.SignUpVerificationData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);

            if (userList.Any())
            {
                var user = userList.First();
                var userVerifyCodeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == user.UserId);

                //xử lý khi người dùng nhấn lần 2 sau khi mã hết hạn và bị xóa
                if (userVerifyCodeList.Count() == 0) return new CodeExpiredResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Mã xác minh không tồn tại",
                };

                var userVerifyCode = userVerifyCodeList.First();


                if (userVerifyCode.Code != data.VerificationCode)
                {
                    return new BaseResponse<UserDTO>() { IsSuccess = false, Message = "Sai mã xác nhận" };
                }

                if (userVerifyCode.VerificationCodeExpireTime <= DateTime.Now)
                {
                    await _unitOfWork.VerificationCodeRepository.Delete(userVerifyCode);
                    await _unitOfWork.Save();
                    return new CodeExpiredResponse<UserDTO>()
                    {
                        IsSuccess = false,
                        Message = "Mã xác minh đã hết hạn",
                        IsExpired = true
                    };
                }

                user.IsVerified = true;
                user.CreatedAt = DateTime.Now;
                userVerifyCode.Code = null;

                await _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.VerificationCodeRepository.Delete(userVerifyCode);
                await _unitOfWork.Save();

                return new BaseResponse<UserDTO>() { IsSuccess = true, Message = "Tài khoản của bạn đã được xác minh thành công" };
            }

            return new BaseResponse<UserDTO>() { IsSuccess = false, Message = "Không tìm thấy mail" };
        }
    }
}
