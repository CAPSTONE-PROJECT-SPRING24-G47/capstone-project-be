using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetCommentsByTouristAttractionIdHandler : IRequestHandler<GetCommentsByTouristAttractionIdRequest, BaseResponse<TouristAttractionCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentsByTouristAttractionIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TouristAttractionCommentDTO>> Handle(GetCommentsByTouristAttractionIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionId, out int TouristAttractionId))
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var comments = await _unitOfWork.TouristAttractionCommentRepository.
                Find(rc => rc.TouristAttractionId == TouristAttractionId);

            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<TouristAttractionCommentDTO>>(comments)
            };
        }
    }
}
