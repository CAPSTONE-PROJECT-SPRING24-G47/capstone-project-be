using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionCategories.Requests
{
    public class GetTADetailCategoriesRequest(string id) : IRequest<object>
    {
        public string Id { get; set; } = id;
    }
}
