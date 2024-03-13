using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Handles
{
    public class UpdateTripHandler : IRequestHandler<UpdateTripRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTripHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateTripRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<TripDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip = _mapper.Map<Trip>(request.TripData);
            trip.TripId = tripId;

            var trip_LocationList = await _unitOfWork.Trip_LocationRepository.
                Find(tc => tc.TripId == tripId);
            await _unitOfWork.Trip_LocationRepository.DeleteRange(trip_LocationList);
            trip_LocationList = trip.Trip_Locations.ToList();
            await _unitOfWork.Trip_LocationRepository.AddRange(trip_LocationList);

            var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
                Find(ta => ta.TripId == tripId);
            await _unitOfWork.Trip_AccommodationRepository.DeleteRange(trip_AccommodationList);
            trip_AccommodationList = trip.Trip_Accommodations.ToList();
            await _unitOfWork.Trip_AccommodationRepository.AddRange(trip_AccommodationList);

            var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
                Find(tr => tr.TripId == tripId);
            await _unitOfWork.Trip_RestaurantRepository.DeleteRange(trip_RestaurantList);
            trip_RestaurantList = trip.Trip_Restaurants.ToList();
            await _unitOfWork.Trip_RestaurantRepository.AddRange(trip_RestaurantList);

            var trip_TouristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                Find(tta => tta.TripId == tripId);
            await _unitOfWork.Trip_TouristAttractionRepository.DeleteRange(trip_TouristAttractionList);
            trip_TouristAttractionList = trip.Trip_TouristAttractions.ToList();
            await _unitOfWork.Trip_TouristAttractionRepository.AddRange(trip_TouristAttractionList);

            await _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.Save();

            return new BaseResponse<TripDTO>()
            {
                IsSuccess = true,
                Message = "Update chuyến đi thành công"
            };
        }
    }
}
