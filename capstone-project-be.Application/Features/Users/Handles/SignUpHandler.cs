﻿using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
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

            //await _emailSender.SendEmail(email, "Verify Code", $"Your verification code is {verifyCodeGenerated}");

            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);

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

                return $"Mã xác minh đã được gửi lại vào mail {data.Email}";
            }

            if (userList.Any(user => user.IsVerified)) return "Email này đã được sử dụng ở một tài khoản khác";

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
