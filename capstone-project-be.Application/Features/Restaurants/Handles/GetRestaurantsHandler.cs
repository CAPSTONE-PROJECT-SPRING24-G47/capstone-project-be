using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantPhotos;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class GetRestaurantsHandler : IRequestHandler<GetRestaurantsRequest, IEnumerable<RestaurantDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetRestaurantsHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDTO>> Handle(GetRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurants = _mapper.Map<IEnumerable<RestaurantDTO>>
                (await _unitOfWork.RestaurantRepository.GetAll());

            foreach (var item in restaurants)
            {
                var restaurantPhotoList = _mapper.Map<IEnumerable<RestaurantPhotoDTO>>
                (await _unitOfWork.RestaurantPhotoRepository.
                Find(rp => rp.RestaurantId == item.RestaurantId));

                foreach (var photo in restaurantPhotoList)
                {
                    photo.SignedUrl = await _storageRepository.GetSignedUrlAsync(photo.SavedFileName);
                }

                item.RestaurantPhotos = restaurantPhotoList;

                var res_resCategoryList = _mapper.Map<IEnumerable<CRUDRes_ResCategoryDTO>>
                    (await _unitOfWork.Res_ResCategoryRepository.
                    Find(res => res.RestaurantId == item.RestaurantId));
                item.Restaurant_RestaurantCategories = res_resCategoryList;
            }

            return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);
        }

    }
}
