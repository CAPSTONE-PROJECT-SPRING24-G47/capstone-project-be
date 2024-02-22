using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class GetTouristAttractionHandler : IRequestHandler<GetTouristAttractionRequest, BaseResponse<TouristAttractionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTouristAttractionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TouristAttractionDTO>> Handle(GetTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionId, out int touristAttractionId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttraction = await _unitOfWork.TouristAttractionRepository.GetByID(touristAttractionId);
            if (touristAttraction == null)
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy địa điểm du lịch!"
                };
            }

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Data = new List<TouristAttractionDTO> { _mapper.Map<TouristAttractionDTO>(touristAttraction) }
            };
        }
    }
}
