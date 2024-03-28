using AutoMapper;
using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class GetBlogCommentsHandler : IRequestHandler<GetBlogCommentsRequest, IEnumerable<BlogCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogCommentDTO>> Handle(GetBlogCommentsRequest request, CancellationToken cancellationToken)
        {
            var blogComments = await _unitOfWork.BlogCommentRepository.GetAll();
            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            blogComments = blogComments.OrderByDescending(bc => bc.CreatedAt).Skip(skip).Take(pageSize);


            return _mapper.Map<IEnumerable<BlogCommentDTO>>(blogComments);
        }
    }
}
