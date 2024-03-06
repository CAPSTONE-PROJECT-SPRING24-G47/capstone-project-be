using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionCategories;
using capstone_project_be.Application.Features.TouristAttractionCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionCategories.Handles
{
    public class GetTouristAttractionCategoriesHandler : IRequestHandler<GetTouristAttractionCategoriesRequest, IEnumerable<TouristAttractionCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTouristAttractionCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TouristAttractionCategoryDTO>> Handle(GetTouristAttractionCategoriesRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionCategories = await _unitOfWork.TouristAttractionCategoryRepository.GetAll();

            return _mapper.Map<IEnumerable<TouristAttractionCategoryDTO>>(touristAttractionCategories);
        }
    }
}
