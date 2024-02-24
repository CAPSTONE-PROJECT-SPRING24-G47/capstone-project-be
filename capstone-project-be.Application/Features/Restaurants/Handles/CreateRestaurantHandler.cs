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
        private readonly IMapper _mapper;

        public CreateRestaurantHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
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
                restaurant.status = "Approved";
            }
            else restaurant.status = "Processing";

            await _unitOfWork.RestaurantRepository.Add(restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nhà hàng mới thành công"
            };
        }
    }
}
