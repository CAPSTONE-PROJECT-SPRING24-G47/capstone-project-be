using AutoMapper;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetNumberOfCommentsByRestaurantIdHandler : IRequestHandler<GetNumberOfCommentsByRestaurantIdRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfCommentsByRestaurantIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfCommentsByRestaurantIdRequest request, CancellationToken cancellationToken)
        {
            var restaurantId = int.Parse(request.RestaurantId);

            var comments = await _unitOfWork.RestaurantCommentRepository.
                Find(rc => rc.RestaurantId == restaurantId);

            var result = comments.ToList().Count();

            return result;
        }
    }
}
