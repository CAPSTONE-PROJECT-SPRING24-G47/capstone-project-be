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
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Handles
{
    public class GetTripsHandler : IRequestHandler<GetTripsRequest, IEnumerable<TripDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTripsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TripDTO>> Handle(GetTripsRequest request, CancellationToken cancellationToken)
        {
            var tripList = await _unitOfWork.TripRepository.GetAll();
            var trips = _mapper.Map<IEnumerable<TripDTO>>(tripList);

            foreach (var item in trips)
            {
                var tripId = item.TripId;

                var trip_LocationList = await _unitOfWork.Trip_LocationRepository.
                Find(tl => tl.TripId == tripId);
                item.Trip_Locations = _mapper.Map<IEnumerable<CRUDTrip_LocationDTO>>(trip_LocationList);

                var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
                GetAccommodationsByTripId(tripId);
                item.Trip_Accommodations = _mapper.Map<IEnumerable<CRUDTrip_AccommodationDTO>>(trip_AccommodationList);

                var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
                GetRestaurantsByTripId(tripId);
                item.Trip_Restaurants = _mapper.Map<IEnumerable<CRUDTrip_RestaurantDTO>>(trip_RestaurantList);

                var trip_touristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                    GetTouristAttractionsByTripId(tripId);
                item.Trip_TouristAttractions = _mapper.Map<IEnumerable<CRUDTrip_TouristAttractionDTO>>(trip_touristAttractionList);
            }
            return trips;
        }

    }
}
