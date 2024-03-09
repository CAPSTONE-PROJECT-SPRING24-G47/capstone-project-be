using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantCategories.Requests
{
    public class GetRestaurantDetailCategoriesRequest(string id) : IRequest<object>
    {
        public string Id { get; set; } = id;
    } 
}
