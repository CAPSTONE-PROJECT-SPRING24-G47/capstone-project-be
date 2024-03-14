using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.Features.BlogCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogCategories.Handles
{
    public class GetBlogDetailCategoriesHandler : IRequestHandler<GetBlogDetailCategoriesRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetBlogDetailCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(GetBlogDetailCategoriesRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int blogId))
            {
                return new BaseResponse<ReadBlog_BlogCategoryDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var list = await _unitOfWork.Blog_BlogCategoryRepository.
                GetBlogDetailCategories(blogId);

            return new BaseResponse<ReadBlog_BlogCategoryDTO>()
            {
                IsSuccess = true,
                Data = list
            };
        }
    }
}
