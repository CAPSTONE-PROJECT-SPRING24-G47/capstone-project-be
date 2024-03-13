using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Handles
{
    public class GetAccommodationByTripIdHandler : IRequestHandler<GetAccommodationByTripIdRequest, BaseResponse<AccommodationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccommodationByTripIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccommodationDTO>> Handle(GetAccommodationByTripIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip_Accommodations = await _unitOfWork.Trip_AccommodationRepository.
                Find(ta => ta.TripId == tripId);
            if(!trip_Accommodations.Any()) 
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Không có địa điểm phù hợp",
                    IsSuccess = false
                };
            }

            var accommodationIds = new List<int>();
            foreach(var item in trip_Accommodations)
            {
                accommodationIds.Add(item.AccommodationId);
            }

            var accommodations = new List<AccommodationDTO>(); 
            foreach (var id in accommodationIds)
            {
                var accommodationList = await _unitOfWork.AccommodationRepository.
                    Find(acc => acc.AccommodationId == id);
                var accommodation = _mapper.Map<AccommodationDTO>(accommodationList.First());
                accommodations.Add(accommodation);
            }

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Data = accommodations
            };
        }
    }
}
