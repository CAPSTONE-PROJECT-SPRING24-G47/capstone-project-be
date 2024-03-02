using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests
{
    public class UpdateAcc_AccCategoryRequest(string Id, CRUDAcc_AccCategoryDTO updateAcc_AccCategoryData) : IRequest<object>
    {
        public CRUDAcc_AccCategoryDTO UpdateAcc_AccCategoryData { get; set; } = updateAcc_AccCategoryData;
        public string Id { get; set; } = Id;
    }
}
