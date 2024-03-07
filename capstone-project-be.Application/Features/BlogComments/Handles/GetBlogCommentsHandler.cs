using AutoMapper;
using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
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
            var BlogComments = await _unitOfWork.BlogCommentRepository.GetAll();

            return _mapper.Map<IEnumerable<BlogCommentDTO>>(BlogComments);
        }
    }
}
