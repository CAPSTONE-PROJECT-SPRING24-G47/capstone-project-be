using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
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

            await _unitOfWork.RestaurantRepository.Add(restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nhà hàng mới thành công"
            };
        }
    }
}
