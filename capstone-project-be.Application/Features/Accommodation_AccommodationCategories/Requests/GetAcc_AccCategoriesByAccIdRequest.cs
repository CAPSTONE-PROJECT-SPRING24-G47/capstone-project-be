using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests
{
    public class GetAcc_AccCategoriesByAccIdRequest(string accommodationId) : IRequest<IEnumerable<Acc_AccCategoryDTO>>
    {
        public string AccommodationId { get; set; } = accommodationId;
    }
}
