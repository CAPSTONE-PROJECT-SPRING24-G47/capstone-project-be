using AutoMapper;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Features.Trip_TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_TouristAttractions.Handles
{
    public class CreateTrip_TouristAttractionHandler : IRequestHandler<CreateTrip_TouristAttractionRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTrip_TouristAttractionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateTrip_TouristAttractionRequest request, CancellationToken cancellationToken)
        {
            var trip_TouristAttractionData = request.Trip_TouristAttractionData;

            var tripList = await _unitOfWork.TripRepository.
                Find(t => t.TripId == trip_TouristAttractionData.TripId);
            if (!tripList.Any())
            {
                return new BaseResponse<Trip_TouristAttractionDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại chuyến đi với Id :{trip_TouristAttractionData.TripId}"
                };
            }

            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.
                Find(ta => ta.TouristAttractionId == trip_TouristAttractionData.TouristAttractionId);
            if (!touristAttractionList.Any())
            {
                return new BaseResponse<Trip_TouristAttractionDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại địa điểm giải trí với Id :{trip_TouristAttractionData.TouristAttractionId}"
                };
            }

            var trip_TouristAttraction = _mapper.Map<Trip_TouristAttraction>(trip_TouristAttractionData);

            var trip_TouristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
            Find(ta => ta.TouristAttractionId == trip_TouristAttraction.TouristAttractionId);

            if (trip_TouristAttractionList.Any()) return new BaseResponse<Trip_TouristAttractionDTO>()
            {
                IsSuccess = false,
                Message = "Địa điểm giải trí đã tồn tại trong chuyến đi"
            };

            await _unitOfWork.Trip_TouristAttractionRepository.Add(trip_TouristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<Trip_TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Thêm địa điểm giải trí mới cho chuyến đi"
            };
        }
    }
}
