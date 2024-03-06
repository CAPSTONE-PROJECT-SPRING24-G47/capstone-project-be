using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationCategories;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationCategories.Handles
{
    public class GetAccommodationCategoriesHandler : IRequestHandler<GetAccommodationCategoriesRequest, IEnumerable<AccommodationCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccommodationCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccommodationCategoryDTO>> Handle(GetAccommodationCategoriesRequest request, CancellationToken cancellationToken)
        {
            var accommodationCategories = await _unitOfWork.AccommodationCategoryRepository.GetAll();

            return _mapper.Map<IEnumerable<AccommodationCategoryDTO>>(accommodationCategories);
        }
    }
}
