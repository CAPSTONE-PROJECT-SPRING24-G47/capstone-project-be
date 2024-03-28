using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class GetCommentsByBlogIdHandler : IRequestHandler<GetCommentsByBlogIdRequest, BaseResponse<BlogCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentsByBlogIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BlogCommentDTO>> Handle(GetCommentsByBlogIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogId, out int blogId))
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var comments = await _unitOfWork.BlogCommentRepository.
                Find(bc => bc.BlogId == blogId);

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            comments = comments.OrderByDescending(c => c.CreatedAt).Skip(skip).Take(pageSize);

            return new BaseResponse<BlogCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<BlogCommentDTO>>(comments)
            };
        }
    }
}
