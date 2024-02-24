using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class ApproveCreateRestaurantHandler : IRequestHandler<ApproveCreateRestaurantRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApproveCreateRestaurantHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(ApproveCreateRestaurantRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int restaurantId))
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var restaurantList = await _unitOfWork.RestaurantRepository.Find(r => r.RestaurantId == restaurantId);

            if (!restaurantList.Any()) return new BaseResponse<RestaurantDTO>()
            {
                Message = "Không tìm thấy nhà hàng",
                IsSuccess = false
            };

            var restaurant = restaurantList.First();
            var action = request.Action;
            if (action.Equals("Approve"))
            {
                restaurant.Status = "Approved";
            }
            else restaurant.Status = "Denied";
            await _unitOfWork.RestaurantRepository.Update(restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantDTO>()
            {
                Message = "Yêu cầu được phê duyệt",
                IsSuccess = true
            };
        }
    }
}
