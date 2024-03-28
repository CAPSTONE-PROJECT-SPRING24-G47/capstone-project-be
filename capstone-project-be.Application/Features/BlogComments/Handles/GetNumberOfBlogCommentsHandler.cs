using AutoMapper;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class GetNumberOfBlogCommentsHandler : IRequestHandler<GetNumberOfBlogCommentsRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfBlogCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfBlogCommentsRequest request, CancellationToken cancellationToken)
        {
            var blogComment = await _unitOfWork.BlogCommentRepository.GetAll();

            var result = blogComment.ToList().Count();

            return result;
        }
    }
}
