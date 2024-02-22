using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class GetRestaurantsHandler : IRequestHandler<GetRestaurantsRequest, IEnumerable<RestaurantDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantDTO>> Handle(GetRestaurantsRequest request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.RestaurantRepository.GetAll();

            return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurant);
        }

    }
}
