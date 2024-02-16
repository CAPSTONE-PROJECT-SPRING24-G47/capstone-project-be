using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResetPasswordHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var data = request.ResetPasswordData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);
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
