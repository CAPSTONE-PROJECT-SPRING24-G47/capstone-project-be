using AutoMapper;
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
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurantRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateRestaurantHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateRestaurantRequest request, CancellationToken cancellationToken)
        {
            var restaurantData = request.RestaurantData;
            var restaurant = _mapper.Map<Restaurant>(restaurantData);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == restaurant.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {restaurant.UserId}"
                };
            }
            var user = userList.First();
            if (user.RoleId == 3)
            {
                restaurant.Status = "Approved";
                restaurant.CreatedAt = DateTime.Now;
            }
            else restaurant.Status = "Processing";
            restaurant.CreatedAt = DateTime.Now;
            string[] ranges = restaurant.PriceRange.Split('-');
            var min = int.Parse(ranges[0]);
            var max = int.Parse(ranges[1]);
            var average = (min + max) / 2;
            if (average < 500000) restaurant.PriceLevel = "Giá thấp";
            else if (average < 1000000) restaurant.PriceLevel = "Trung bình";
            else restaurant.PriceLevel = "Giá cao";

            await _unitOfWork.RestaurantRepository.Add(restaurant);
            await _unitOfWork.Save();

            var restaurantList = (await _unitOfWork.RestaurantRepository.
                Find(r => r.UserId == restaurant.UserId)).OrderByDescending(r => r.CreatedAt);
            var restaurantId = restaurantList.First().RestaurantId;
            var res_ResCategoryData = restaurantData.Res_ResCategories;
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
            var res_ResCategoryList = _mapper.Map<IEnumerable<Restaurant_RestaurantCategory>>(res_ResCategories.Distinct());
            await _unitOfWork.Res_ResCategoryRepository.AddRange(res_ResCategoryList);

            var photoData = restaurantData.Photos;
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
            var restaurantPhotoList = _mapper.Map<IEnumerable<RestaurantPhoto>>(restaurantPhotos);
            await _unitOfWork.RestaurantPhotoRepository.AddRange(restaurantPhotoList);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nhà hàng mới thành công"
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
