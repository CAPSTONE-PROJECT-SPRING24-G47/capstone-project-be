using AutoMapper;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class GetNumberOfCommentsByBlogIdHandler : IRequestHandler<GetNumberOfCommentsByBlogIdRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfCommentsByBlogIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfCommentsByBlogIdRequest request, CancellationToken cancellationToken)
        {
            var blogId = int.Parse(request.BlogId);

            var comments = await _unitOfWork.BlogCommentRepository.
                Find(bc => bc.BlogId == blogId);

            var result = comments.ToList().Count();

            return result;
        }
    }
}
