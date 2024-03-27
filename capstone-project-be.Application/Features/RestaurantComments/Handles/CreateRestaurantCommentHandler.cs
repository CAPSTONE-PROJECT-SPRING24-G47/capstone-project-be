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
    public class CreateRestaurantCommentHandler : IRequestHandler<CreateRestaurantCommentRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateRestaurantCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(CreateRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            var restaurantCommentData = request.RestaurantCommentData;
            var restaurantComment = _mapper.Map<RestaurantComment>(restaurantCommentData);
            restaurantComment.IsReported = false;
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
                    Message = $"Không tồn tại Restaurant với Id : {restaurantComment.RestaurantId}"
                };
            }

            await _unitOfWork.RestaurantCommentRepository.Add(restaurantComment);
            await _unitOfWork.Save();

            restaurantComment = (await _unitOfWork.RestaurantCommentRepository.
                Find(rc => rc.UserId == restaurantComment.UserId)).OrderByDescending(rc => rc.CreatedAt).First();
            var restaurantCommentId = restaurantComment.RestaurantCommentId;
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
                await _unitOfWork.Save();
            }
            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
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
