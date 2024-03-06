using capstone_project_be.Application.DTOs.RestaurantCategories;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantCategories.Requests
{
    public class GetRestaurantCategoriesRequest : IRequest<IEnumerable<RestaurantCategoryDTO>>
    {
    }
}
