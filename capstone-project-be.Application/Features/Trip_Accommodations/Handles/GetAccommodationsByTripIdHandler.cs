using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Handles
{
    public class GetAccommodationsByTripIdHandler : IRequestHandler<GetAccommodationsByTripIdRequest, BaseResponse<CRUDTrip_AccommodationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccommodationsByTripIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CRUDTrip_AccommodationDTO>> Handle(GetAccommodationsByTripIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<CRUDTrip_AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip_Accommodations = await _unitOfWork.Trip_AccommodationRepository.
                Find(ta => ta.TripId == tripId);
            if(!trip_Accommodations.Any()) 
            {
                return new BaseResponse<CRUDTrip_AccommodationDTO>()
                {
                    Message = "Không có nơi ở nào trong chuyến đi này",
                    IsSuccess = false
                };
            }

            var accommodationIds = new List<int>();
            foreach(var item in trip_Accommodations)
            {
                accommodationIds.Add(item.AccommodationId);
            }

            var trip_AccommodationList = new List<CRUDTrip_AccommodationDTO>(); 
            foreach (var id in accommodationIds)
            {
                var accommodationList = await _unitOfWork.AccommodationRepository.
                    Find(acc => acc.AccommodationId == id);
                var tripAcc = _mapper.Map<CRUDTrip_AccommodationDTO>(accommodationList.First());
                var trip_Accommodation = await _unitOfWork.Trip_AccommodationRepository.
                        Find(tr => tr.TripId == tripId && tr.AccommodationId == id);
                var Id = trip_Accommodation.First().Id;
                tripAcc.Id = Id;
                trip_AccommodationList.Add(tripAcc);
            }

            return new BaseResponse<CRUDTrip_AccommodationDTO>()
            {
                IsSuccess = true,
                Data = trip_AccommodationList
            };
        }
    }
}
