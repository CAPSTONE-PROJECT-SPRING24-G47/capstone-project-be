using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests
{
    public class GetRes_ResCategoriesByResIdRequest(string restaurantId) : IRequest<IEnumerable<Res_ResCategoryDTO>>
    {
        public string RestaurantId { get; set; } = restaurantId;
    }
}
