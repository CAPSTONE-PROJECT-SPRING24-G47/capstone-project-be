using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;
using System.Reflection.Metadata;

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

            foreach (var item in blogs)
            {
                var blogId = item.BlogId;
                var blogPhotoList = await _unitOfWork.BlogPhotoRepository.
                Find(bp => bp.BlogId == blogId);
                item.BlogPhotos = blogPhotoList;
                var blog_BlogCategoryList = await _unitOfWork.Blog_BlogCategoryRepository.
                    Find(bbc => bbc.BlogId == blogId);
                item.Blog_BlogCatagories = blog_BlogCategoryList;
            }

            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
    }
}
