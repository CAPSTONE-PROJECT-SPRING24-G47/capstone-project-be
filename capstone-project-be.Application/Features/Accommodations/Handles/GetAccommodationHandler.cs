using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class GetAccommodationHandler : IRequestHandler<GetAccommodationRequest, BaseResponse<AccommodationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccommodationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccommodationDTO>> Handle(GetAccommodationRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationId, out int accommodationId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodation = await _unitOfWork.AccommodationRepository.GetByID(accommodationId);
            if (accommodation == null)
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy nơi ở!"
                };
            }

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Data = new List<AccommodationDTO> { _mapper.Map<AccommodationDTO>(accommodation) }
            };
        }
    }
}
