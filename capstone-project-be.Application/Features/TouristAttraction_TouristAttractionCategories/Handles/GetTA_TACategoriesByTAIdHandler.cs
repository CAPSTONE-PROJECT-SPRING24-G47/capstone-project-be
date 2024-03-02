using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Handles
{
    public class GetTA_TACategoriesByTAIdHandler : IRequestHandler<GetTA_TACategoriesByTAIdRequest, IEnumerable<TA_TACategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTA_TACategoriesByTAIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TA_TACategoryDTO>> Handle(GetTA_TACategoriesByTAIdRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionId = int.Parse(request.TouristAttractionId);
            var tA_TACategoryList = await _unitOfWork.TA_TACategoryRepository.Find(ta => ta.TouristAttractionId == touristAttractionId);

            return _mapper.Map<IEnumerable<TA_TACategoryDTO>>(tA_TACategoryList);
        }
    }
}
