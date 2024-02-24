using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
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

            var users = await _unitOfWork.UserRepository.Find(user => user.UserId == userId);
            User user;

            if (!users.Any()) return new BaseResponse<UserDTO>()
            {
                Message = "Không tìm thấy người dùng",
                IsSuccess = false
            };

            var newPass = request.Password;
            user = users.First();
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(newPass, 13);
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
