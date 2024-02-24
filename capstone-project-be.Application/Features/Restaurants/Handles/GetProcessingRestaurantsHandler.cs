using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class GetProcessingRestaurantsHandler : IRequestHandler<GetProcessingRestaurantsRequest, IEnumerable<RestaurantDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProcessingRestaurantsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDTO>> Handle(GetProcessingRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurantList = await _unitOfWork.RestaurantRepository.Find(r => r.Status == "Processing");

            return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurantList);
        }
    }
}
