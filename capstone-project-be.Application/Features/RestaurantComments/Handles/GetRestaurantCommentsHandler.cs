using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetRestaurantCommentsHandler : IRequestHandler<GetRestaurantCommentsRequest, IEnumerable<RestaurantCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RestaurantCommentDTO>> Handle(GetRestaurantCommentsRequest request, CancellationToken cancellationToken)
        {
            var restaurantComments = await _unitOfWork.RestaurantCommentRepository.GetAll();
            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            restaurantComments = restaurantComments.Skip(skip).Take(pageSize).OrderByDescending(rc => rc.CreatedAt);

            return _mapper.Map<IEnumerable<RestaurantCommentDTO>>(restaurantComments);
        }
    }
}
