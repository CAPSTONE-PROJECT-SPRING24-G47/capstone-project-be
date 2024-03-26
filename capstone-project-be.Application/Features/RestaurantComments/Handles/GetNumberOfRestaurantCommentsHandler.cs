using AutoMapper;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetNumberOfRestaurantCommentsHandler : IRequestHandler<GetNumberOfRestaurantCommentsRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfRestaurantCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfRestaurantCommentsRequest request, CancellationToken cancellationToken)
        {
            var restaurantComment = await _unitOfWork.RestaurantCommentRepository.GetAll();

            var result = restaurantComment.ToList().Count();

            return result;
        }
    }
}
