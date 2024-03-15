using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Features.Trip_TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_TouristAttractions.Handles
{
    public class GetTouristAttractionsByTripIdHandler : IRequestHandler<GetTouristAttractionsByTripIdRequest, BaseResponse<CRUDTrip_TouristAttractionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTouristAttractionsByTripIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CRUDTrip_TouristAttractionDTO>> Handle(GetTouristAttractionsByTripIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<CRUDTrip_TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip_TouristAttractions = await _unitOfWork.Trip_TouristAttractionRepository.
                Find(tta => tta.TripId == tripId);
            if (!trip_TouristAttractions.Any())
            {
                return new BaseResponse<CRUDTrip_TouristAttractionDTO>()
                {
                    Message = "Không có địa điểm giải trí nào trong chuyến đi này",
                    IsSuccess = false
                };
            }

            var touristAttractionIds = new List<int>();
            foreach (var item in trip_TouristAttractions)
            {
                touristAttractionIds.Add(item.TouristAttractionId);
            }

            var trip_TouristAttractionList = new List<CRUDTrip_TouristAttractionDTO>();
            foreach (var id in touristAttractionIds)
            {
                var touristAttractionList = await _unitOfWork.TouristAttractionRepository.
                    Find(ta => ta.TouristAttractionId == id);
                var tripTa = _mapper.Map<CRUDTrip_TouristAttractionDTO>(touristAttractionList.First());
                var trip_TouristAttraction = await _unitOfWork.Trip_TouristAttractionRepository.
                        Find(tr => tr.TripId == tripId && tr.TouristAttractionId == id);
                var Id = trip_TouristAttraction.First().Id;
                tripTa.Id = Id;
                trip_TouristAttractionList.Add(tripTa);
            }

            return new BaseResponse<CRUDTrip_TouristAttractionDTO>()
            {
                IsSuccess = true,
                Data = trip_TouristAttractionList
            };
        }
    }
}
