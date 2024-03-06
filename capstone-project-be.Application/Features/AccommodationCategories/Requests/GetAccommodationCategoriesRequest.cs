using capstone_project_be.Application.DTOs.AccommodationCategories;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationCategories.Requests
{
    public class GetAccommodationCategoriesRequest : IRequest<IEnumerable<AccommodationCategoryDTO>>
    {
    }
}
