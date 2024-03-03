using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests
{
    public class UpdateRes_ResCategoryRequest(string Id, CRUDRes_ResCategoryDTO updateRes_ResCategoryData) : IRequest<object>
    {
        public CRUDRes_ResCategoryDTO UpdateRes_ResCategoryData { get; set; } = updateRes_ResCategoryData;
        public string Id { get; set; } = Id;
    }
}
