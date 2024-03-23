using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Handles
{
    public class SignInHandler : IRequestHandler<SignInRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public SignInHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var data = request.UserSignInData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email && user.IsVerified == true);
            if (!userList.Any())
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Tài khoản hoặc mật khẩu không đúng!"
                };
            else
            {
                var user = userList.First();
                if (!string.IsNullOrWhiteSpace(user.SavedFileName))
                {
                    user.SignedUrl = await _storageRepository.GetSignedUrlAsync(user.SavedFileName);
                }
                bool isPasswordMatch = BCrypt.Net.BCrypt.EnhancedVerify(data.Password, user.Password);
                if (isPasswordMatch)
                    return new BaseResponse<UserDTO>()
                    {
                        IsSuccess = true,
                        Message = "Đăng nhập thành công!",
                        Data = new List<UserDTO> { _mapper.Map<UserDTO>(user) }
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
