using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class GetRestaurantCommentHandler : IRequestHandler<GetRestaurantCommentRequest, BaseResponse<RestaurantCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRestaurantCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<RestaurantCommentDTO>> Handle(GetRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantCommentId, out int RestaurantCommentId))
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var RestaurantComment = await _unitOfWork.RestaurantCommentRepository.GetByID(RestaurantCommentId);

            if (RestaurantComment == null)
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy comment!"
                };
            }

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Data = new List<RestaurantCommentDTO> { _mapper.Map<RestaurantCommentDTO>(RestaurantComment) }
            };
        }
    }
}
