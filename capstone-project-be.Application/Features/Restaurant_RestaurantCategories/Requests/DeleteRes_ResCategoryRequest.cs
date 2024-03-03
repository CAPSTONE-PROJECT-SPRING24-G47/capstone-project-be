using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests
{
    public class DeleteRes_ResCategoryRequest(string Id) : IRequest<object>
    {
        public string Id { get; set; } = Id;
    }
}
