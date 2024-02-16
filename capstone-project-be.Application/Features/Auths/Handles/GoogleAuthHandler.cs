using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Auths.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Handles
{
    public class GoogleAuthHandler : IRequestHandler<GoogleAuthRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GoogleAuthHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GoogleAuthRequest request, CancellationToken cancellationToken)
        {
            var data = request.GoogleAuthData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.Email == data.Email);

            if (userList.Any(user => user.IsVerified == true && user.IsGoogleAuth == false))
            {
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = "Email đã được sử dụng ở tài khoản khác"
                };
            }
            else if (userList.Any(user => user.IsVerified == true && user.IsGoogleAuth == true))
            {
                var user = userList.First();
                var userDTO = _mapper.Map<UserDTO>(user);
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = true,
                    Message = "Đăng nhập thành công",
                    Data = new List<UserDTO> { userDTO }
                };
            }
            else if (userList.Any(user => user.IsVerified == false))
            {
                var user = userList.First();
                user.IsVerified = true;
                user.IsGoogleAuth = true;
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.PictureProfile = data.PictureProfile;
                user.CreatedAt = DateTime.Now;
                user.Password = "";

                var userVerificationCodeList = await _unitOfWork.VerificationCodeRepository.Find(code => code.UserId == user.UserId);
                var userVerificationCode = userVerificationCodeList.First();
                await _unitOfWork.VerificationCodeRepository.Delete(userVerificationCode);
                await _unitOfWork.Save();

                await _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.Save();

                var userDTO = _mapper.Map<UserDTO>(user);
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = true,
                    Message = "Đăng ký thành công",
                    Data = new List<UserDTO> { userDTO }
                };
            }

            var userMapped = _mapper.Map<User>(data);
            userMapped.IsVerified = true;
            userMapped.IsGoogleAuth = true;
            userMapped.Password = "";
            userMapped.CreatedAt = DateTime.Now;
            await _unitOfWork.UserRepository.Add(userMapped);
            await _unitOfWork.Save();
            userList = await _unitOfWork.UserRepository.Find(user => user.Email == userMapped.Email);

            return new BaseResponse<UserDTO>()
            {
                IsSuccess = true,
                Message = "Đăng ký thành công",
                Data = new List<UserDTO> { _mapper.Map<UserDTO>(userList.First()) }
            };
        }
    }
}
