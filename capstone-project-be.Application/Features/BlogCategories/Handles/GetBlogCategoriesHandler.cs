using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationCategories;
using capstone_project_be.Application.DTOs.BlogCategories;
using capstone_project_be.Application.Features.BlogCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.BlogCategories.Handles
{
    public class GetBlogCategoriesHandler : IRequestHandler<GetBlogCategoriesRequest, IEnumerable<BlogCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogCategoryDTO>> Handle(GetBlogCategoriesRequest request, CancellationToken cancellationToken)
        {
            var blogCategories = await _unitOfWork.BlogCategoryRepository.GetAll();

            return _mapper.Map<IEnumerable<BlogCategoryDTO>>(blogCategories);
        }
    }
}
