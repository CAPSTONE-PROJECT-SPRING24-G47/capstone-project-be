using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantPhotos;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class GetRestaurantHandler : IRequestHandler<GetRestaurantRequest, BaseResponse<RestaurantDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetRestaurantHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<RestaurantDTO>> Handle(GetRestaurantRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantId, out int restaurantId))
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var restaurant = _mapper.Map<RestaurantDTO>
                (await _unitOfWork.RestaurantRepository.GetByID(restaurantId));
            if (restaurant == null)
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy nhà hàng!"
                };
            }

            var restaurantPhotoList = _mapper.Map<IEnumerable<CRUDRestaurantPhotoDTO>>
                (await _unitOfWork.RestaurantPhotoRepository.
                Find(ap => ap.RestaurantId == restaurantId));

            foreach (var item in restaurantPhotoList)
            {
                item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
                item.FileAsBase64 = await _storageRepository.GetFileAsBase64Async(item.SavedFileName);
            }

            restaurant.RestaurantPhotos = restaurantPhotoList;
            var acc_accCategoryList = _mapper.Map<IEnumerable<CRUDRes_ResCategoryDTO>>
                (await _unitOfWork.Res_ResCategoryRepository.
                Find(res => res.RestaurantId == restaurantId));
            restaurant.Restaurant_RestaurantCategories = acc_accCategoryList;

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Data = new List<RestaurantDTO> { restaurant }
            };
        }
    }
}
