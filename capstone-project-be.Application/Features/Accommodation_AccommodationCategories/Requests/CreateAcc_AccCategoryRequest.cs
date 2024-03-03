using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests
{
    public class CreateAcc_AccCategoryRequest(CRUDAcc_AccCategoryDTO acc_accCategoryData) : IRequest<object>
    {
        public CRUDAcc_AccCategoryDTO Acc_AccCategoryData { get; set; } = acc_accCategoryData;
    }
}
