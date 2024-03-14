using MediatR;

namespace capstone_project_be.Application.Features.BlogCategories.Requests
{
    public class GetBlogDetailCategoriesRequest(string id) : IRequest<object>
    {
        public string Id { get; set; } = id;
    }
}
