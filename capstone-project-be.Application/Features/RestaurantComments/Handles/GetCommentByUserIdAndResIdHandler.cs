using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetCommentByUserIdAndResIdHandler : IRequestHandler<GetCommentByUserIdAndResIdRequest, BaseResponse<RestaurantCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentByUserIdAndResIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<RestaurantCommentDTO>> Handle(GetCommentByUserIdAndResIdRequest request, CancellationToken cancellationToken)
        {
            var restaurantId = int.Parse(request.RestaurantId);
            var userId = int.Parse(request.UserId);

            var comments = await _unitOfWork.RestaurantCommentRepository.
                Find(rc => rc.RestaurantId == restaurantId && rc.UserId == userId);
            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<RestaurantCommentDTO>>(comments)
            };
        }
    }
}
