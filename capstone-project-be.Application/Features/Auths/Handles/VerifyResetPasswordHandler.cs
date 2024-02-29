using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Handles
{
    public class VerifyResetPasswordHandler : IRequestHandler<VerifyResetPasswordRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public VerifyResetPasswordHandler(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }
        public async Task<object> Handle(VerifyResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var data = request.ResetPasswordData;
            var email = request.ResetPasswordData.Email;
            var verifyCodeGenerated = GenerateVerificationCode();
            var expireTime = DateTime.Now.AddMinutes(1);

            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);

            if (userList.Any())
            {
                //await _emailSender.SendEmail(email, "Reset Password Code", $"Your reset password code is {verifyCodeGenerated}");
                var userId = userList.First().UserId;
                var codeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == userId);
                if (codeList.Any())
                {
                    var codeToUpdate = codeList.First();
                    codeToUpdate.UserId = userId;
                    codeToUpdate.Code = verifyCodeGenerated;
                    codeToUpdate.VerificationCodeExpireTime = expireTime;
                    await _unitOfWork.VerificationCodeRepository.Update(codeToUpdate);
                    await _unitOfWork.Save();
                }
                else
                {
                    await _unitOfWork.VerificationCodeRepository.Add(
                         new VerificationCode()
                         {
                             UserId = userId,
                             Code = verifyCodeGenerated,
                             VerificationCodeExpireTime = expireTime
                         });
                    await _unitOfWork.Save();
                }
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = true,
                    Message = $"Mã xác minh đã được gửi lại vào mail {data.Email}"
                };
            }

            else return new BaseResponse<UserDTO>()
            {
                IsSuccess = false,
                Message = "Email không tồn tại!"
            };
        }

        private string GenerateVerificationCode()
        {
            Random rnd = new Random();
            int code = rnd.Next(1000000, 9999999);

            return code.ToString();
        }
    }
}
