using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests
{
    public class GetAcc_AccCategoriesRequest : IRequest<IEnumerable<Acc_AccCategoryDTO>>
    {
    }
}
