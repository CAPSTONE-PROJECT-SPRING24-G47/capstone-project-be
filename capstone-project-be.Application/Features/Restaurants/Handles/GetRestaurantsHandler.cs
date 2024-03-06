using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class GetRestaurantsHandler : IRequestHandler<GetRestaurantsRequest, IEnumerable<RestaurantDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDTO>> Handle(GetRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurants = await _unitOfWork.RestaurantRepository.GetAll();

            foreach (var item in restaurants)
            {
                var restaurantId = item.RestaurantId;
                var restaurantPhotoList = await _unitOfWork.RestaurantPhotoRepository.
                Find(rp => rp.RestaurantId == restaurantId);
                item.RestaurantPhotos = restaurantPhotoList;
                var res_ResCategoryList = await _unitOfWork.Res_ResCategoryRepository.
                    Find(rrc => rrc.RestaurantId == restaurantId);
                item.Restaurant_RestaurantCategories = res_ResCategoryList;
            }

            return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
        }

    }
}
