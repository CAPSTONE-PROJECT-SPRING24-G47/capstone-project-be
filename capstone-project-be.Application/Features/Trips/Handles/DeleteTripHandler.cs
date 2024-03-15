using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Handles
{
    public class DeleteTripHandler : IRequestHandler<DeleteTripRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTripHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTripRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<TripDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var tripList = await _unitOfWork.TripRepository.Find(t => t.TripId == tripId);

            if (tripList.Count() == 0) return new BaseResponse<TripDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy chuyến đi với Id: {tripId}"
            };

            var trip = tripList.First();

            var trip_Locations = await _unitOfWork.Trip_LocationRepository.Find(tl => tl.TripId == tripId);
            await _unitOfWork.Trip_LocationRepository.DeleteRange(trip_Locations);

            var trip_Accommodations = await _unitOfWork.Trip_AccommodationRepository.Find(ta => ta.TripId == tripId);
            await _unitOfWork.Trip_AccommodationRepository.DeleteRange(trip_Accommodations);

            var trip_Restaurants = await _unitOfWork.Trip_RestaurantRepository.Find(tr => tr.TripId == tripId);
            await _unitOfWork.Trip_RestaurantRepository.DeleteRange(trip_Restaurants);

            var trip_TouristAttractions = await _unitOfWork.Trip_TouristAttractionRepository.Find(tta => tta.TripId == tripId);
            await _unitOfWork.Trip_TouristAttractionRepository.DeleteRange(trip_TouristAttractions);

            await _unitOfWork.TripRepository.Delete(trip);
            await _unitOfWork.Save();

            return new BaseResponse<TripDTO>()
            {
                IsSuccess = true,
                Message = "Xóa chuyến đi thành công"
            };
        }
    }
}
