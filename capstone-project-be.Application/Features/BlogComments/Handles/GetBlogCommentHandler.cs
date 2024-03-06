using AutoMapper;
using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class GetBlogCommentHandler : IRequestHandler<GetBlogCommentRequest, BaseResponse<BlogCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BlogCommentDTO>> Handle(GetBlogCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogCommentId, out int BlogCommentId))
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var BlogComment = await _unitOfWork.BlogCommentRepository.GetByID(BlogCommentId);

            if (BlogComment == null)
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy comment!"
                };
            }

            return new BaseResponse<BlogCommentDTO>()
            {
                IsSuccess = true,
                Data = new List<BlogCommentDTO> { _mapper.Map<BlogCommentDTO>(BlogComment) }
            };
        }
    }
}
