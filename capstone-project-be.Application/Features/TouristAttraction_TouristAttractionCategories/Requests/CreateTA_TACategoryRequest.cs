using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests
{
    public class CreateTA_TACategoryRequest(CRUDTA_TACategoryDTO tA_TACategoryData) : IRequest<object>
    {
        public CRUDTA_TACategoryDTO TA_TACategoryData { get; set; } = tA_TACategoryData;
    }
}
