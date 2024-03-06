using AutoMapper;
using capstone_project_be.Application.DTOs.BlogCategories;
using capstone_project_be.Application.DTOs.RestaurantCategories;
using capstone_project_be.Application.Features.RestaurantCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantCategories.Handles
{
    public class GetRestaurantCategoriesHandler : IRequestHandler<GetRestaurantCategoriesRequest, IEnumerable<RestaurantCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantCategoryDTO>> Handle(GetRestaurantCategoriesRequest request, CancellationToken cancellationToken)
        {
            var restaurantCategories = await _unitOfWork.RestaurantCategoryRepository.GetAll();

            return _mapper.Map<IEnumerable<RestaurantCategoryDTO>>(restaurantCategories);
        }
    }
}
