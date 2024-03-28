using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetCommentByUserIdAndTAIdHandler : IRequestHandler<GetCommentByUserIdAndTAIdRequest, BaseResponse<TouristAttractionCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentByUserIdAndTAIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TouristAttractionCommentDTO>> Handle(GetCommentByUserIdAndTAIdRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionId = int.Parse(request.TouristAttractionId);
            var userId = int.Parse(request.UserId);

            var comments = await _unitOfWork.TouristAttractionCommentRepository.
                Find(tac=> tac.TouristAttractionId == touristAttractionId && tac.UserId == userId);
            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<TouristAttractionCommentDTO>>(comments)
            };
        }
    }
}
