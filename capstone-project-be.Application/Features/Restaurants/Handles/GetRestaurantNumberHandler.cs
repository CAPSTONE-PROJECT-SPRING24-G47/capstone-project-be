using AutoMapper;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Handles
{
    public class GetRestaurantNumberHandler : IRequestHandler<GetRestaurantNumberRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantNumberHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetRestaurantNumberRequest request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.RestaurantRepository.GetAll();

            var result = restaurant.ToList().Count();

            return result;
        }
    }
}
