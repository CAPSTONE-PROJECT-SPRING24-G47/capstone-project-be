using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            var data = request.UpdateProfileData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.UserId == data.UserId);
            if (userList.Any())
            {
                var userToUpdate = userList.First();
                userToUpdate.FirstName = data.FirstName;
                userToUpdate.LastName = data.LastName;
                userToUpdate.PictureProfile = data.PictureProfile;
                await _unitOfWork.UserRepository.Update(userToUpdate);
                await _unitOfWork.Save();
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = true,
                    Message = "Cập nhật thành công !"
                };

            }
            return new BaseResponse<UserDTO>()
            {
                IsSuccess = false,
                Message = "Cập nhật thất bại !"
            };
        }
    }
}
