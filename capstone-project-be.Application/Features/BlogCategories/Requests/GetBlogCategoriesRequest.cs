using capstone_project_be.Application.DTOs.BlogCategories;
using MediatR;

namespace capstone_project_be.Application.Features.BlogCategories.Requests
{
    public class GetBlogCategoriesRequest : IRequest<IEnumerable<BlogCategoryDTO>>
    {

    }
}
