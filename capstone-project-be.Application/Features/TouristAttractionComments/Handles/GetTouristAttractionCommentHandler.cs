using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetTouristAttractionCommentHandler : IRequestHandler<GetTouristAttractionCommentRequest, BaseResponse<TouristAttractionCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTouristAttractionCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TouristAttractionCommentDTO>> Handle(GetTouristAttractionCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionCommentId, out int TouristAttractionCommentId))
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var TouristAttractionComment = await _unitOfWork.TouristAttractionCommentRepository.GetByID(TouristAttractionCommentId);

            if (TouristAttractionComment == null)
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy comment!"
                };
            }

            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Data = new List<TouristAttractionCommentDTO> { _mapper.Map<TouristAttractionCommentDTO>(TouristAttractionComment) }
            };
        }
    }
}
