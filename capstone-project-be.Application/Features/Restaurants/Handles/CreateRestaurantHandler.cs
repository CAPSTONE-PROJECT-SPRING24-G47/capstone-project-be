using AutoMapper;
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
            await _unitOfWork.RestaurantRepository.Add(restaurant);
            await _unitOfWork.Save();

            var restaurantList = await _unitOfWork.RestaurantRepository.
                Find(r => r.UserId == restaurant.UserId && r.CreatedAt >= DateTime.Now.AddMinutes(-1));
            if (!restaurantList.Any())
                return new BaseResponse<RestaurantDTO>()
                {
                    IsSuccess = false,
                    Message = "Thêm nhà hàng mới thất bại"
                };
            var restaurantId = restaurantList.First().RestaurantId;
            var res_ResCategories = restaurantData.Restaurant_RestaurantCategories;
            var res_ResCategoryList = _mapper.Map<IEnumerable<Restaurant_RestaurantCategory>>(res_ResCategories);
            foreach (var item in res_ResCategoryList)
            {
                item.RestaurantId = restaurantId;
            }
            await _unitOfWork.Res_ResCategoryRepository.AddRange(res_ResCategoryList);

            var restaurantPhotos = restaurantData.RestaurantPhotos;
            var restaurantPhotoList = _mapper.Map<IEnumerable<RestaurantPhoto>>(restaurantPhotos);
            foreach (var item in restaurantPhotoList)
            {
                item.RestaurantId = restaurantId;
            }
            await _unitOfWork.RestaurantPhotoRepository.AddRange(restaurantPhotoList);

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nhà hàng mới thành công"
            };
        }
    }
}
