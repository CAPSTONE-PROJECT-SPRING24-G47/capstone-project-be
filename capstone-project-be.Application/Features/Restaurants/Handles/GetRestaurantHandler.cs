using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetRestaurantHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
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

            var restaurant = await _unitOfWork.RestaurantRepository.GetByID(restaurantId);
            if (restaurant == null)
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy nhà hàng!"
                };
            }

            return new BaseResponse<RestaurantDTO>()
            {
                IsSuccess = true,
                Data = new List<RestaurantDTO> { _mapper.Map<RestaurantDTO>(restaurant) }
            };
        }
    }
}
