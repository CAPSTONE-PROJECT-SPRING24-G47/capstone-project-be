using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class SignInHandler : IRequestHandler<SignInRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SignInHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var data = request.UserSignInData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);
            if (!userList.Any())
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Tài khoản hoặc mật khẩu không đúng!"
                };
            else
            {
                bool isPasswordMatch = BCrypt.Net.BCrypt.EnhancedVerify(data.Password, userList.First().Password);
                if (isPasswordMatch)
                    return new BaseResponse<UserDTO>()
                    {
                        IsSuccess = true,
                        Message = "Đăng nhập thành công!"
                    };
                else
                    return new BaseResponse<UserDTO>()
                    {
                        IsSuccess = false,
                        Message = "Tài khoản hoặc mật khẩu không đúng!"
                    };
            }

        }
    }
}
