using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;
using System.Text.RegularExpressions;

namespace capstone_project_be.Application.Features.Auths.Handles
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string PASSWORD_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{6,}$";

        public ResetPasswordHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<object> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var data = request.ResetPasswordData;
            var userList = await _unitOfWork.UserRepository.Find(user => 
                user.Email == data.Email && user.IsVerified && !user.IsGoogleAuth);

            Regex passwordRegex = new Regex(PASSWORD_REGEX);

            if (!passwordRegex.IsMatch(data.Password))
            {
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Mật khẩu không đáp ứng yêu cầu",
                };
            }


            if (!userList.Any())
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Xảy ra lỗi khi cập nhật mật khẩu !"
                };

            else
            {
                var userToUpdate = userList.First();
                var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(data.Password, 13);
                userToUpdate.Password = passwordHash;
                await _unitOfWork.UserRepository.Update(userToUpdate);
                await _unitOfWork.Save();
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = true,
                    Message = "Cập nhật mật khẩu thành công !",
                };
            }

        }
    }
}
