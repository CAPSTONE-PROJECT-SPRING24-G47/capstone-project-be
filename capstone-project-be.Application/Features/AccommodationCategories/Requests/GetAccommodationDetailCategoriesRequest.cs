using MediatR;

namespace capstone_project_be.Application.Features.AccommodationCategories.Requests
{
    public class GetAccommodationDetailCategoriesRequest(string id) : IRequest<object>
    {
        public string Id { get; set; } = id;
    }
}
 