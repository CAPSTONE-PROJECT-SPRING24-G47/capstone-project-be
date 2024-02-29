using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;
using System.Text.RegularExpressions;

namespace capstone_project_be.Application.Features.Auths.Handles
{
    public class SignUpHandler : IRequestHandler<SignUpRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private const string NAME_REGEX = @"^[\p{L} ]{1,20}$";
        private const string EMAIL_REGEX = @"^(?![0-9])[^@\s]+@[^\s@]+\.[^\s@]+$";
        private const string PASSWORD_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{6,}$";

        public SignUpHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<object> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            var data = request.UserSignUpData;

            var email = request.UserSignUpData.Email;
            var verifyCodeGenerated = GenerateVerificationCode();
            var expireTime = DateTime.Now.AddMinutes(1);

            //await _emailSender.SendEmail(email, "Verify Code", $"Your verification code is {verifyCodeGenerated}");

            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);
            Regex nameRegex = new Regex(NAME_REGEX);
            Regex emailRegex = new Regex(EMAIL_REGEX);
            Regex passwordRegex = new Regex(PASSWORD_REGEX);

            if (!passwordRegex.IsMatch(data.Password) || !nameRegex.IsMatch(data.LastName)
                || !nameRegex.IsMatch(data.FirstName) || !emailRegex.IsMatch(data.Email))
            {
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Một hoặc nhiều trường không đáp ứng yêu cầu",
                };
            }

            if (userList.Any(user => !user.IsVerified))
            {
                var userToUpdate = userList.First();
                var userVerifyCodeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == userToUpdate.UserId);
                VerificationCode userVerifyCodeToUpdate;
                if (userVerifyCodeList.Any())
                {
                    userVerifyCodeToUpdate = userVerifyCodeList.First();
                    userVerifyCodeToUpdate.Code = verifyCodeGenerated;
                    userVerifyCodeToUpdate.VerificationCodeExpireTime = expireTime;

                    var passwordEncrypt = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password, 13);

                    userToUpdate.LastName = data.LastName;
                    userToUpdate.FirstName = data.FirstName;
                    userToUpdate.Password = passwordEncrypt;

                    await _unitOfWork.UserRepository.Update(userToUpdate);
                    await _unitOfWork.Save();
                    await _unitOfWork.VerificationCodeRepository.Update(userVerifyCodeToUpdate);
                    await _unitOfWork.Save();
                }
                else
                {
                    await _unitOfWork.VerificationCodeRepository.Add(
                         new VerificationCode()
                         {
                             UserId = userToUpdate.UserId,
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

            if (userList.Any(user => user.IsVerified))
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Email này đã được sử dụng ở một tài khoản khác"
                };


            var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password, 13);
            var userMapped = _mapper.Map<User>(data);
            userMapped.Password = passwordHash;

            await _unitOfWork.UserRepository.Add(userMapped);
            await _unitOfWork.Save();
            await _unitOfWork.VerificationCodeRepository.Add(
                  new VerificationCode()
                  {
                      UserId = userMapped.UserId,
                      Code = verifyCodeGenerated,
                      VerificationCodeExpireTime = expireTime
                  });
            await _unitOfWork.Save();

            return new BaseResponse<UserDTO>()
            {
                IsSuccess = true,
                Message = $"Đăng ký thành công, mã xác minh đã được gửi vào {data.Email}",
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
