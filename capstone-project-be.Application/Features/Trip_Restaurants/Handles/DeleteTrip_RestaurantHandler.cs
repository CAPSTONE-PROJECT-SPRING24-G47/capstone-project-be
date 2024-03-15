using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Restaurants.Handles
{
    public class DeleteTrip_RestaurantHandler : IRequestHandler<DeleteTrip_RestaurantRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTrip_RestaurantHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTrip_RestaurantRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int trip_RestaurantId))
            {
                return new BaseResponse<Trip_RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
                Find(tr => tr.Id == trip_RestaurantId);

            var trip_Restaurant = trip_RestaurantList.First();

            await _unitOfWork.Trip_RestaurantRepository.Delete(trip_Restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<Trip_RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Xóa nhà hàng trong chuyến đi thành công"
            };
        }
    }
}
