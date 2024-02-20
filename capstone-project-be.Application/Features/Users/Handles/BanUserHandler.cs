using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class BanUserHandler : IRequestHandler<BanUserRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BanUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(BanUserRequest request, CancellationToken cancellationToken)
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

            if(!users.Any()) return new BaseResponse<UserDTO>()
            {
                Message = "Không tìm thấy người dùng",
                IsSuccess = false
            };

            user = users.First();
            user.Status = !user.Status;
            await _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Save();

            return new BaseResponse<UserDTO>()
            {
                Message = user.Status ? $"Người dùng {user.UserId} đã hết bị cấm" : $"Người dùng {user.UserId} đã bị cấm",
                IsSuccess = true
            };
        }
    }
}
