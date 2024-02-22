using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class DeleteRestaurantHandler : IRequestHandler<DeleteRestaurantRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRestaurantHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteRestaurantRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantId, out int restaurantId))
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var restaurantList = await _unitOfWork.RestaurantRepository.Find(r => r.RestaurantId == restaurantId);

            if (restaurantList.Count() == 0) return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy nhà hàng với Id: {restaurantId}"
            };

            var restaurant = restaurantList.First();

            await _unitOfWork.RestaurantRepository.Delete(restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Xóa nhà hàng thành công"
            };
        }
    }
}
