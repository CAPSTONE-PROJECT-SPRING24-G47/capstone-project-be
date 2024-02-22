using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class UpdateResutaurantHandler : IRequestHandler<UpdateRestaurantRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateResutaurantHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
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

            var restaurant = _mapper.Map<Restaurant>(request.RestaurantData);
            restaurant.RestaurantId = restaurantId;

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

            await _unitOfWork.RestaurantRepository.Update(restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Update nhà hàng thành công"
            };
        }
    }
}
