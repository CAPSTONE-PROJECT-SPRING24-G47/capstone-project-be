using AutoMapper;
using BCrypt.Net;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace capstone_project_be.Application.Features.Users.Handles
{
    //IRequestHandler<t1,t2(optional)>
    //t1 là kiểu request thằng handle này sẽ nhận để xử lý
    //t2 là kiểu dữ liệu trả về (trong trường hợp này k có tại không trả về cái gì)
    public class SignUpHandler : IRequestHandler<SignUpRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public SignUpHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<string> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            var data = request.UserSignUpData;

            var email = request.UserSignUpData.Email;
            var verifyCodeGenerated = GenerateVerificationCode();
            var expireTime = DateTime.Now.AddMinutes(1);

            await _emailSender.SendEmail(email, "Verify Code", $"Your verification code is {verifyCodeGenerated}");

            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);

            if (userList.Any(user => !user.IsVerified))
            {
                var userToUpdate = userList.First();
                userToUpdate.VerificationCode = verifyCodeGenerated;
                userToUpdate.VerificationCodeExpireTime  = expireTime;
                await _unitOfWork.UserRepository.Update(userToUpdate);
                await _unitOfWork.Save();
                return $"Mã xác minh đã được gửi lại vào mail {data.Email}";
            }

            if (userList.Any(user => user.IsVerified)) return "Email này đã được sử dụng ở một tài khoản khác";

            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password, 13);
            var userMapped = _mapper.Map<User>(data);

            userMapped.VerificationCode = verifyCodeGenerated;
            userMapped.VerificationCodeExpireTime = expireTime;
            userMapped.Password = passwordHash;
            await _unitOfWork.UserRepository.Add(userMapped);
            var isSuccessSave = await _unitOfWork.Save();
            if (isSuccessSave == 0)
            {
                return "Đăng ký không thành công";
            }
            return "Đăng ký thành công";
        }

        private string GenerateVerificationCode()
        {
            Random rnd = new Random();
            int code = rnd.Next(1000000, 9999999);

            return code.ToString();
        }


    }
}
