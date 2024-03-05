using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetProcessingBlogsHandler : IRequestHandler<GetProcessingBlogsRequest, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProcessingBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDTO>> Handle(GetProcessingBlogsRequest request, CancellationToken cancellationToken)
        {
            var blogList = await _unitOfWork.BlogRepository.Find(b => b.Status.Trim().ToLower() == "Processing".Trim().ToLower());

            return _mapper.Map<IEnumerable<BlogDTO>>(blogList);
        }
    }
}
