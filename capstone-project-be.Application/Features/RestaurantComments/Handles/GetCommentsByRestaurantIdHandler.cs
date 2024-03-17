using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetCommentsByRestaurantIdHandler : IRequestHandler<GetCommentsByRestaurantIdRequest, BaseResponse<RestaurantCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentsByRestaurantIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<RestaurantCommentDTO>> Handle(GetCommentsByRestaurantIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantId, out int RestaurantId))
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var comments = await _unitOfWork.RestaurantCommentRepository.
                Find(rc => rc.RestaurantId == RestaurantId);

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<RestaurantCommentDTO>>(comments)
            };
        }
    }
}
