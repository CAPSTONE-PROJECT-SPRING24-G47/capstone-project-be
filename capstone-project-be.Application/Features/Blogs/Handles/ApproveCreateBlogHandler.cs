using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class ApproveCreateBlogHandler : IRequestHandler<ApproveCreateBlogRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApproveCreateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(ApproveCreateBlogRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int blogId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var blogList = await _unitOfWork.BlogRepository.Find(b => b.BlogId == blogId);

            if (!blogList.Any()) return new BaseResponse<BlogDTO>()
            {
                Message = "Không tìm thấy blog",
                IsSuccess = false
            };

            var blog = blogList.First();
            var action = request.Action;
            if (action.Equals("Approve"))
            {
                blog.Status = "Approved";
                await _unitOfWork.BlogRepository.Update(blog);
                await _unitOfWork.Save();

                return new BaseResponse<BlogDTO>()
                {
                    Message = "Yêu cầu được phê duyệt",
                    IsSuccess = true
                };
            }
            else blog.Status = "Rejected";
            await _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.Save();

            return new BaseResponse<BlogDTO>()
            {
                Message = "Yêu cầu bị từ chối",
                IsSuccess = true
            };
        }
    }
}
