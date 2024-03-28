using AutoMapper;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetNumberOfBlogByUserIdHandler : IRequestHandler<GetNumberOfBlogByUserIdRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfBlogByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfBlogByUserIdRequest request, CancellationToken cancellationToken)
        {
            var blogs = _mapper.Map<IEnumerable<BlogDTO>>(await _unitOfWork.BlogRepository.
                Find(b => b.UserId == int.Parse(request.UserId)));

            var result = blogs.ToList().Count();

            return result;
        }
    }
}
