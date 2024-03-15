using AutoMapper;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Restaurants.Handles
{
    public class CreateTrip_RestaurantHandler : IRequestHandler<CreateTrip_RestaurantRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTrip_RestaurantHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateTrip_RestaurantRequest request, CancellationToken cancellationToken)
        {
            var trip_RestaurantData = request.Trip_RestaurantData;

            var tripList = await _unitOfWork.TripRepository.
                Find(t => t.TripId == trip_RestaurantData.TripId);
            if (!tripList.Any())
            {
                return new BaseResponse<Trip_RestaurantDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại chuyến đi với Id :{trip_RestaurantData.TripId}"
                };
            }

            var restaurantList = await _unitOfWork.RestaurantRepository.
                Find(r => r.RestaurantId == trip_RestaurantData.RestaurantId);
            if (!restaurantList.Any())
            {
                return new BaseResponse<Trip_RestaurantDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại nhà hàng với Id :{trip_RestaurantData.RestaurantId}"
                };
            }

            var trip_Restaurant = _mapper.Map<Trip_Restaurant>(trip_RestaurantData);

            var trip_RestaurantList = await _unitOfWork.Trip_RestaurantRepository.
            Find(tr => tr.RestaurantId == trip_Restaurant.RestaurantId);

            if (trip_RestaurantList.Any()) return new BaseResponse<Trip_RestaurantDTO>()
            {
                IsSuccess = false,
                Message = "Nhà hàng đã tồn tại trong chuyến đi"
            };

            await _unitOfWork.Trip_RestaurantRepository.Add(trip_Restaurant);
            await _unitOfWork.Save();

            return new BaseResponse<Trip_RestaurantDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nhà hàng mới cho chuyến đi"
            };
        }
    }
}
