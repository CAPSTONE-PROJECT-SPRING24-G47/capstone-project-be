using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantPhotos;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurantRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateRestaurantHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateRestaurantRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantId, out int restaurantId))
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var restaurantData = request.UpdateRestaurantData;
            var restaurant = _mapper.Map<Restaurant>(restaurantData);
            restaurant.RestaurantId = restaurantId;
            restaurant.CreatedAt = DateTime.Now;

            var cityId = restaurant.CityId;
            var cityList = await _unitOfWork.CityRepository.Find(c => c.CityId == cityId);
            if (!cityList.Any())
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại thành phố với CityId : {cityId}"
                };
            }

            var res_ResCategoryList = await _unitOfWork.Res_ResCategoryRepository.Find(res => res.RestaurantId == restaurantId);
            await _unitOfWork.Res_ResCategoryRepository.DeleteRange(res_ResCategoryList);
            var res_ResCategoryData = restaurantData.Res_ResCategories;
            if (res_ResCategoryData != null)
            {
                string[] parts = res_ResCategoryData.Split(',');
                var res_ResCategories = new List<CRUDRes_ResCategoryDTO>();
                foreach (string part in parts)
                {
                    res_ResCategories.Add(
                        new CRUDRes_ResCategoryDTO
                        {
                            RestaurantId = restaurantId,
                            RestaurantCategoryId = int.Parse(part)
                        });
                }
                res_ResCategoryList = _mapper.Map<IEnumerable<Restaurant_RestaurantCategory>>(res_ResCategories.Distinct());
                await _unitOfWork.Res_ResCategoryRepository.AddRange(res_ResCategoryList);
            }

            var deletePhotos = restaurantData.DeletePhotos;
            if (deletePhotos != null)
            {
                var deletePhotoIds = new List<int>();
                var parts = deletePhotos.Split(",");
                foreach (string part in parts)
                {
                    deletePhotoIds.Add(int.Parse(part));
                }
                var restaurantPhotoList = await _unitOfWork.RestaurantPhotoRepository.
                    Find(rp => rp.RestaurantId == restaurantId && deletePhotoIds.Contains(rp.RestaurantPhotoId));

                foreach (var rp in restaurantPhotoList)
                {
                    await _storageRepository.DeleteFileAsync(rp.SavedFileName);
                }
                await _unitOfWork.RestaurantPhotoRepository.DeleteRange(restaurantPhotoList);
                var photoData = restaurantData.Photos;
                if (photoData != null)
                {
                    var restaurantPhotos = new List<CRUDRestaurantPhotoDTO>();
                    foreach (var photo in photoData)
                    {
                        if (photo != null)
                        {
                            var savedFileName = GenerateFileNameToSave(photo.FileName);
                            restaurantPhotos.Add(
                                new CRUDRestaurantPhotoDTO
                                {
                                    RestaurantId = restaurantId,
                                    PhotoURL = await _storageRepository.UpLoadFileAsync(photo, savedFileName),
                                    SavedFileName = savedFileName
                                }
                                );
                        }
                    }
                    restaurantPhotoList = _mapper.Map<IEnumerable<RestaurantPhoto>>(restaurantPhotos);
                    await _unitOfWork.RestaurantPhotoRepository.AddRange(restaurantPhotoList);
                }
            }

            await _unitOfWork.RestaurantRepository.Update(restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Update nhà hàng thành công"
            };
        }
        private string? GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
