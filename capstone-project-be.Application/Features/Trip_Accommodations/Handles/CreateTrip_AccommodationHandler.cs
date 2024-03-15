using AutoMapper;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Handles
{
    public class CreateTrip_AccommodationHandler : IRequestHandler<CreateTrip_AccommodationRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTrip_AccommodationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateTrip_AccommodationRequest request, CancellationToken cancellationToken)
        {
            var trip_AccommodationData = request.Trip_AccommodationData;

            var tripList = await _unitOfWork.TripRepository.
                Find(t => t.TripId == trip_AccommodationData.TripId);
            if (!tripList.Any())
            {
                return new BaseResponse<Trip_AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại chuyến đi với Id :{trip_AccommodationData.TripId}"
                };
            }

            var accommodationList = await _unitOfWork.AccommodationRepository.
                Find(a => a.AccommodationId == trip_AccommodationData.AccommodationId);
            if (!accommodationList.Any())
            {
                return new BaseResponse<Trip_AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại nơi ở với Id :{trip_AccommodationData.AccommodationId}"
                };
            }

            var trip_Accommodation = _mapper.Map<Trip_Accommodation>(trip_AccommodationData);

            var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
            Find(ta => ta.AccommodationId == trip_Accommodation.AccommodationId);

            if (trip_AccommodationList.Any()) return new BaseResponse<Trip_AccommodationDTO>()
            {
                IsSuccess = false,
                Message = "Nơi ở đã tồn tại trong chuyến đi"
            };

            await _unitOfWork.Trip_AccommodationRepository.Add(trip_Accommodation);
            await _unitOfWork.Save();

            return new BaseResponse<Trip_AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nơi ở mới cho chuyến đi"
            };
        }
    }
}
