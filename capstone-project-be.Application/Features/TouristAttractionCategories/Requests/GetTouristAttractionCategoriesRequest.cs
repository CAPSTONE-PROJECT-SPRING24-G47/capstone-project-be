using capstone_project_be.Application.DTOs.TouristAttractionCategories;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionCategories.Requests
{
    public class GetTouristAttractionCategoriesRequest : IRequest<IEnumerable<TouristAttractionCategoryDTO>>
    {
    }
}
