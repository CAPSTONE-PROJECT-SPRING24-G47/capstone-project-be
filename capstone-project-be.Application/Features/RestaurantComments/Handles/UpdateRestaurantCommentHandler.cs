using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantCommentPhotos;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class UpdateRestaurantCommentHandler : IRequestHandler<UpdateRestaurantCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }


        public async Task<object> Handle(UpdateRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantCommentId, out int restaurantCommentId))
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var restaurantCommentData = request.UpdateRestaurantCommentData;
            var restaurantComment = _mapper.Map<RestaurantComment>(restaurantCommentData);
            restaurantComment.RestaurantCommentId = restaurantCommentId;
            restaurantComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == restaurantComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {restaurantComment.UserId}"
                };
            }

            var RestaurantList = await _unitOfWork.RestaurantRepository.Find(a => a.RestaurantId == restaurantComment.RestaurantId);
            if (!RestaurantList.Any())
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại nhà hàng với Id : {restaurantComment.RestaurantId}"
                };
            }

            var deletePhotos = restaurantCommentData.DeletePhotos;
            if (deletePhotos != null)
            {
                var deletePhotoIds = new List<int>();
                var parts = deletePhotos.Split(",");
                foreach (string part in parts)
                {
                    deletePhotoIds.Add(int.Parse(part));
                }
                var restaurantCommentPhotoList = await _unitOfWork.RestaurantCommentPhotoRepository.
                    Find(rcp => rcp.RestaurantCommentId == restaurantCommentId && deletePhotoIds.Contains(rcp.Id));

                foreach (var rcp in restaurantCommentPhotoList)
                {
                    await _storageRepository.DeleteFileAsync(rcp.SavedFileName);
                }
                await _unitOfWork.RestaurantCommentPhotoRepository.DeleteRange(restaurantCommentPhotoList);
            }

            var photoData = restaurantCommentData.Photos;
            if (photoData != null)
            {
                var restaurantCommentPhotos = new List<CRUDRestaurantCommentPhotoDTO>();
                foreach (var photo in photoData)
                {
                    if (photo != null)
                    {
                        var savedFileName = GenerateFileNameToSave(photo.FileName);
                        restaurantCommentPhotos.Add(
                            new CRUDRestaurantCommentPhotoDTO
                            {
                                RestaurantCommentId = restaurantCommentId,
                                PhotoURL = await _storageRepository.UpLoadFileAsync(photo, savedFileName),
                                SavedFileName = savedFileName
                            }
                            );
                    }
                }
                var restaurantCommentPhotoList = _mapper.Map<IEnumerable<RestaurantCommentPhoto>>(restaurantCommentPhotos);
                await _unitOfWork.RestaurantCommentPhotoRepository.AddRange(restaurantCommentPhotoList);
            }

            await _unitOfWork.RestaurantCommentRepository.Update(restaurantComment);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Message = "Update comment thành công"
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
