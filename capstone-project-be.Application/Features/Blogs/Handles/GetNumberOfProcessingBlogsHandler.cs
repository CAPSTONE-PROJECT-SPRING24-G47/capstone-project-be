using AutoMapper;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetNumberOfProcessingBlogsHandler : IRequestHandler<GetNumberOfProcessingBlogsRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfProcessingBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfProcessingBlogsRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _unitOfWork.BlogRepository.
                Find(b => b.Status.Trim().ToLower() == "Processing".Trim().ToLower());

            var result = blogs.ToList().Count();

            return result;
        }
    }
}
