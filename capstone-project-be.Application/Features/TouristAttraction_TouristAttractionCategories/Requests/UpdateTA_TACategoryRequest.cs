using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests
{
    public class UpdateTA_TACategoryRequest(string Id, CRUDTA_TACategoryDTO updateTA_TACategoryData) : IRequest<object>
    {
        public CRUDTA_TACategoryDTO UpdateTA_TACategoryData { get; set; } = updateTA_TACategoryData;
        public string Id { get; set; } = Id;
    }
}
