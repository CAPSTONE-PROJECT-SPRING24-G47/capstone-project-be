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
            var RestaurantComments = await _unitOfWork.RestaurantCommentRepository.GetAll();

            return _mapper.Map<IEnumerable<RestaurantCommentDTO>>(RestaurantComments);
        }
    }
}
