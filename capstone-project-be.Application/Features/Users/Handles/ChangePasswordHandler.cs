using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChangePasswordHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int userId))
            {
                return new BaseResponse<UserDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var userList = await _unitOfWork.UserRepository.Find(user => user.UserId == userId);

            if (!userList.Any()) return new BaseResponse<UserDTO>()
            {
                Message = "Không tìm thấy người dùng",
                IsSuccess = false
            };
            var user = userList.First();
            bool isPasswordMatch = BCrypt.Net.BCrypt.EnhancedVerify(request.ChangePasswordData.OldPassword, user.Password);
            if (!isPasswordMatch)
                return new BaseResponse<UserDTO>()
                {
                    Message = "Mật khẩu không đúng",
                    IsSuccess = false
                };

            var newPassword = request.ChangePasswordData.NewPassword;
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword, 13);
            await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Save();

            return new BaseResponse<UserDTO>()
            {
                Message = "Cập nhật mật khẩu thành công!",
                IsSuccess = true
            };
        }
    }
}
