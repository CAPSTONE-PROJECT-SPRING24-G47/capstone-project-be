using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Locations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Handles
{
    public class GetTripHandler : IRequestHandler<GetTripRequest, BaseResponse<TripDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTripHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TripDTO>> Handle(GetTripRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<TripDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var tripData = await _unitOfWork.TripRepository.GetByID(tripId);
            if (tripData == null)
            {
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy chuyến đi!"
                };
            }

            var trip = _mapper.Map<TripDTO>(tripData);

            var trip_LocationList = await _unitOfWork.Trip_LocationRepository.
                Find(tl => tl.TripId == tripId);
            trip.Trip_Locations = _mapper.Map<IEnumerable<CRUDTrip_LocationDTO>>(trip_LocationList);

            var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
                GetAccommodationsByTripId(tripId);
            trip.Trip_Accommodations = _mapper.Map <IEnumerable<CRUDTrip_AccommodationDTO>> (trip_AccommodationList);

            var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
                GetRestaurantsByTripId(tripId);
            trip.Trip_Restaurants = _mapper.Map<IEnumerable<CRUDTrip_RestaurantDTO>>(trip_RestaurantList);

            var trip_touristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                GetTouristAttractionsByTripId(tripId);
            trip.Trip_TouristAttractions = _mapper.Map<IEnumerable<CRUDTrip_TouristAttractionDTO>>(trip_touristAttractionList);

            return new BaseResponse<TripDTO>()
            {
                IsSuccess = true,
                Data = new List<TripDTO> { _mapper.Map<TripDTO>(trip) }
            };
        }
    }
}
