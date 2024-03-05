using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    internal class GetBlogsHandler : IRequestHandler<GetBlogsRequest, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDTO>> Handle(GetBlogsRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _unitOfWork.BlogRepository.GetAll();

            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
    }
}
