using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests
{
    public class DeleteAcc_AccCategoryRequest(string Id) : IRequest<object>
    {
        public string Id { get; set; } = Id;
    }
}
