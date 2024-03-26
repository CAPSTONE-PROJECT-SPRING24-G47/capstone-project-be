using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class DeleteRestaurantCommentHandler : IRequestHandler<DeleteRestaurantCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;

        public DeleteRestaurantCommentHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(DeleteRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantCommentId, out int restaurantCommentId))
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var RestaurantCommentList = await _unitOfWork.RestaurantCommentRepository.
                Find(acc => acc.RestaurantCommentId == restaurantCommentId);

            if (RestaurantCommentList.Count() == 0) return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy comment với Id: {restaurantCommentId}"
            };

            var restaurantComment = RestaurantCommentList.First();

            var restaurantCommentPhotos = await _unitOfWork.RestaurantCommentPhotoRepository.
                Find(rcp => rcp.RestaurantCommentId == restaurantCommentId);
            foreach (var item in restaurantCommentPhotos)
            {
                await _storageRepository.DeleteFileAsync(item.SavedFileName);
            }
            await _unitOfWork.RestaurantCommentPhotoRepository.DeleteRange(restaurantCommentPhotos);

            await _unitOfWork.RestaurantCommentRepository.Delete(restaurantComment);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Message = "Xóa comment thành công"
            };
        }
    }
}
