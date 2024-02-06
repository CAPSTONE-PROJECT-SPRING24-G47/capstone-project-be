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

            // Tìm user với mã verify và email
            var userList = await _unitOfWork.UserRepository.Find(user =>
                user.Email == data.Email && user.VerificationCode == data.VerificationCode);

            if (userList.Any())
            {
                var user = userList.First();
                if (user.VerificationCodeExpireTime <= DateTime.Now) return "Mã xác minh đã hết hạn";
            }

            if (userList.Any())
            {
                //Set user verified và xóa verify code trong database
                var user = userList.First();
                user.IsVerified = true;
                user.VerificationCode = null;
                user.CreatedAt = DateTime.Now;
                await _unitOfWork.UserRepository.Update(user);
                var isSuccessUpdate = await _unitOfWork.Save();
                if (isSuccessUpdate != 0)
                    return "Tài khoản của bạn đã được xác minh thành công";
                else return "Có lỗi xảy ra";
            }

            //reuturn message verify code không đúng
            return "Mã xác nhận không hợp lệ";
        }
    }
}
