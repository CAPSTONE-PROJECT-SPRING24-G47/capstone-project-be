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

            var tripData = await _unitOfWork.TripRepository.Find(t => t.TripId == tripId);
            if (!tripData.Any())
            {
                return new BaseResponse<TripDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy chuyến đi!"
                };
            }

            var trip = _mapper.Map<TripDTO>(tripData.First());

            var trip_LocationList = await _unitOfWork.Trip_LocationRepository.
                Find(tl => tl.TripId == tripId);
            trip.Trip_Locations = _mapper.Map<IEnumerable<CRUDTrip_LocationDTO>>(trip_LocationList);

            var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
               GetAccommodationsByTripId(tripId);
            foreach (var tripAcc in trip_AccommodationList)
            {
                var trip_Accommodation = await _unitOfWork.Trip_AccommodationRepository.
                    Find(ta => ta.TripId == tripId && ta.AccommodationId == tripAcc.AccommodationId);
                var Id = trip_Accommodation.First().Id;
                tripAcc.Id = Id;
            }
            trip.Trip_Accommodations = _mapper.Map<IEnumerable<CRUDTrip_AccommodationDTO>>(trip_AccommodationList);

            var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
            GetRestaurantsByTripId(tripId);
            foreach (var tripRes in trip_RestaurantList)
            {
                var trip_Restaurant = await _unitOfWork.Trip_RestaurantRepository.
                    Find(tr => tr.TripId == tripId && tr.RestaurantId == tripRes.RestaurantId);
                var Id = trip_Restaurant.First().Id;
                tripRes.Id = Id;
            }
            trip.Trip_Restaurants = _mapper.Map<IEnumerable<CRUDTrip_RestaurantDTO>>(trip_RestaurantList);

            var trip_touristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                GetTouristAttractionsByTripId(tripId);
            foreach (var tripTa in trip_touristAttractionList)
            {
                var trip_TouristAttraction = await _unitOfWork.Trip_TouristAttractionRepository.
                    Find(tta => tta.TripId == tripId && tta.TouristAttractionId == tripTa.TouristAttractionId);
                var Id = trip_TouristAttraction.First().Id;
                tripTa.Id = Id;
            }
            trip.Trip_TouristAttractions = _mapper.Map<IEnumerable<CRUDTrip_TouristAttractionDTO>>(trip_touristAttractionList);

            return new BaseResponse<TripDTO>()
            {
                IsSuccess = true,
                Data = new List<TripDTO> { trip }
            };
        }
    }
}
