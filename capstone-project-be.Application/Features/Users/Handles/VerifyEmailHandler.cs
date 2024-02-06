using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(VerifyEmailRequest request, CancellationToken cancellationToken)
        {
            var data = request.SignUpVerificationData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);

            if (userList.Any())
            {
                var user = userList.First();
                var userVerifyCodeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == user.UserId);
                var userVerifyCode = userVerifyCodeList.First();

                if (userVerifyCode.VerificationCodeExpireTime <= DateTime.Now)
                {
                    await _unitOfWork.VerificationCodeRepository.Delete(userVerifyCode);
                    await _unitOfWork.Save();
                    return "Mã xác minh đã hết hạn";
                }

                user.IsVerified = true;
                user.CreatedAt = DateTime.Now;
                userVerifyCode.Code = null;

                await _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.VerificationCodeRepository.Delete(userVerifyCode);
                var isSuccessUpdate = await _unitOfWork.Save();

                if (isSuccessUpdate != 0)
                    return "Tài khoản của bạn đã được xác minh thành công";
                else return "Có lỗi xảy ra";
            }

            return "Mã xác nhận không hợp lệ";
        }
    }
}
