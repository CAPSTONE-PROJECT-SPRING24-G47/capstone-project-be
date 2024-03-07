using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class ReportRestaurantHandler : IRequestHandler<ReportRestaurantRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportRestaurantHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(ReportRestaurantRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantId, out int restaurantId))
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var restaurantList = await _unitOfWork.RestaurantRepository.
                Find(acc => acc.RestaurantId == restaurantId);

            if (restaurantList.Count() == 0) return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy nhà hàng với Id: {restaurantId}"
            };

            var restaurant = restaurantList.First();
            restaurant.IsReported = true;

            await _unitOfWork.RestaurantRepository.Update(restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Report thành công"
            };
        }
    }
}
