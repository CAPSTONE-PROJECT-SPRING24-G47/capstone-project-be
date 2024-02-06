using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResetPasswordHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var data = request.ResetPasswordData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);
            if (!userList.Any()) return "Xảy ra lỗi khi cập nhật mật khẩu !";

            var userToUpdate = userList.First();
            var codeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == userToUpdate.UserId);
            var verificationCode = codeList.First();

            if (verificationCode.Code == data.VerificationCode) 
            {
                if (verificationCode.VerificationCodeExpireTime <= DateTime.Now)
                {
                    await _unitOfWork.VerificationCodeRepository.Delete(verificationCode);
                    await _unitOfWork.Save();
                    return "Mã xác minh đã hết hạn";
                }

                if (codeList.Any())
                {
                    var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password, 13);
                    userToUpdate.Password = passwordHash;
                    await _unitOfWork.UserRepository.Update(userToUpdate);
                    await _unitOfWork.Save();
                    return "Cập nhật mật khẩu thành công !";
                }

                else return "Xảy ra lỗi khi cập nhật mật khẩu !";
            }

            else return "Mã xác minh không đúng, vui lòng kiểm tra lại !";
        }
    }
}
