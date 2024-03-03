using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests
{
    public class DeleteTA_TACategoryRequest(string Id) : IRequest<object>
    {
        public string Id { get; set; } = Id;
    }
}
