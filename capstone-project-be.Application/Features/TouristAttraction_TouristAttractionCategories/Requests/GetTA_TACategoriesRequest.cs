using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests
{
    public class GetTA_TACategoriesRequest : IRequest<IEnumerable<TA_TACategoryDTO>>
    {

    }
}
