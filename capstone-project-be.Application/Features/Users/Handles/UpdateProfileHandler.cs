using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateProfileHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            var data = request.UpdateProfileData;
            int id = int.Parse(request.Id);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == id);
            if (!userList.Any())
            {
                return new BaseResponse<UserDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tìm thấy người dùng với Id : {id}"
                };
            }

            var userToUpdate = userList.First();
            userToUpdate.RoleId = data.RoleId;
            userToUpdate.LastName = data.LastName;
            userToUpdate.FirstName = data.FirstName;
            userToUpdate.Email = data.Email;
            userToUpdate.SavedFileName = GenerateFileNameToSave(data.Photo.FileName);
            userToUpdate.PictureProfile = await _storageRepository.UpLoadFileAsync(data.Photo, userToUpdate.SavedFileName);
            await _unitOfWork.UserRepository.Update(userToUpdate);
            await _unitOfWork.Save();

            return new BaseResponse<UserDTO>()
            {
                IsSuccess = true,
                Message = "Cập nhật thông tin thành công"
            };
        }

        private string? GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
