using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
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
                    Message = "Không tìm thấy địa điểm giải trí!"
                };
            }

            var touristAttractionPhotoList = await _unitOfWork.TouristAttractionPhotoRepository.
                Find(tap => tap.TouristAttractionId == touristAttractionId);
            touristAttraction.TouristAttractionPhotos = touristAttractionPhotoList;
            var tA_TACategoryList = await _unitOfWork.TA_TACategoryRepository.
                Find(tac => tac.TouristAttractionId == touristAttractionId);
            touristAttraction.TouristAttraction_TouristAttractionCategories = tA_TACategoryList;

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Data = new List<TouristAttractionDTO> { _mapper.Map<TouristAttractionDTO>(touristAttraction) }
            };
        }
    }
}
