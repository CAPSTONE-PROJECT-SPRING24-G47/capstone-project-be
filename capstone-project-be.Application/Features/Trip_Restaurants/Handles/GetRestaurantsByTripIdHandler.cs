using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Restaurants.Handles
{
    public class GetRestaurantsByTripIdHandler : IRequestHandler<GetRestaurantsByTripIdRequest, BaseResponse<CRUDTrip_RestaurantDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantsByTripIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CRUDTrip_RestaurantDTO>> Handle(GetRestaurantsByTripIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<CRUDTrip_RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip_Restaurants = await _unitOfWork.Trip_RestaurantRepository.
                Find(tr => tr.TripId == tripId);
            if (!trip_Restaurants.Any())
            {
                return new BaseResponse<CRUDTrip_RestaurantDTO>()
                {
                    Message = "Không có nhà hàng nào trong chuyến đi này",
                    IsSuccess = false
                };
            }

            var restaurantIds = new List<int>();
            foreach (var item in trip_Restaurants)
            {
                restaurantIds.Add(item.RestaurantId);
            }

            var trip_RestaurantList = new List<CRUDTrip_RestaurantDTO>();
            foreach (var id in restaurantIds)
            {
                var restaurantList = await _unitOfWork.RestaurantRepository.
                    Find(res => res.RestaurantId == id);
                var tripRes = _mapper.Map<CRUDTrip_RestaurantDTO>(restaurantList.First());
                var trip_Restaurant = await _unitOfWork.Trip_RestaurantRepository.
                        Find(tr => tr.TripId == tripId && tr.RestaurantId == id);
                var Id = trip_Restaurant.First().Id;
                tripRes.Id = Id;
                trip_RestaurantList.Add(tripRes);
            }

            return new BaseResponse<CRUDTrip_RestaurantDTO>()
            {
                IsSuccess = true,
                Data = trip_RestaurantList
            };
        }
    }
}
