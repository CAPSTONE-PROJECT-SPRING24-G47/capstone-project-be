using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class VerifyResetPasswordHandler : IRequestHandler<VerifyResetPasswordRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public VerifyResetPasswordHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }
        public async Task<string> Handle(VerifyResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var data = request.ResetPasswordData;
            var email = request.ResetPasswordData.Email;
            var verifyCodeGenerated = GenerateVerificationCode();

            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);

            if (userList.Any())
            {
                await _emailSender.SendEmail(email, "Reset Password Code", $"Your reset password code is {verifyCodeGenerated}");
                var userId = userList.First().UserId;
                var codeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == userId);
                var userToUpdate = userList.First();
                userToUpdate.VerificationCode = verifyCodeGenerated;
                await _unitOfWork.UserRepository.Update(userToUpdate);
                await _unitOfWork.Save();
                return $"Mã xác minh đã được gửi vào mail {data.Email}";
            }

            else return "Email không tồn tại!";
        }

        private string GenerateVerificationCode()
        {
            Random rnd = new Random();
            int code = rnd.Next(1000000, 9999999);

            return code.ToString();
        }
    }
}
