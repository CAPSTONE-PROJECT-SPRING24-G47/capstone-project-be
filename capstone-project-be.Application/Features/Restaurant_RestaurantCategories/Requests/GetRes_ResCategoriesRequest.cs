using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests
{
    public class GetRes_ResCategoriesRequest : IRequest<IEnumerable<Res_ResCategoryDTO>>
    {
    }
}
