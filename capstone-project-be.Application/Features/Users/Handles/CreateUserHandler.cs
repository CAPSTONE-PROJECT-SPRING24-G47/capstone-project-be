using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userData = request.UserData;
            var user = _mapper.Map<User>(userData);

            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == userData.Email);

            if(userList.Any())
            {
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Email này đã được sử dụng ở một tài khoản khác"
                };
            }

            user.IsBanned = false;
            user.IsGoogleAuth = false;
            user.IsVerified = true;
            user.CreatedAt = DateTime.Now;
            user.Password = BCrypt.Net.BCrypt.EnhancedHashPassword("Abc123@", 13);

            await _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.Save();

            return new BaseResponse<UserDTO>()
            {
                IsSuccess = true,
                Message = "Thêm người dùng mới thành công"
            };
        }
    }
}
