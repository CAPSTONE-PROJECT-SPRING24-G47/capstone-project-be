using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Interfaces;
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
            var trips = await _unitOfWork.TripRepository.GetAll();

            foreach (var item in trips)
            {
                var tripId = item.TripId;
                var trip_LocationList = await _unitOfWork.Trip_LocationRepository.
                Find(tl => tl.TripId == tripId);
                item.Trip_Locations = trip_LocationList;

                var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
                Find(ta => ta.TripId == tripId);
                item.Trip_Accommodations = trip_AccommodationList;

                var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
                Find(tr => tr.TripId == tripId);
                item.Trip_Restaurants = trip_RestaurantList;

                var trip_TouristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                Find(tta => tta.TripId == tripId);
                item.Trip_TouristAttractions = trip_TouristAttractionList;
            }
            return _mapper.Map<IEnumerable<TripDTO>>(trips);
        }

    }
}
