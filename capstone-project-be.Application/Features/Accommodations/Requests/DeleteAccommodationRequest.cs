using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Requests
{
    public class DeleteAccommodationRequest(string accommodationId) : IRequest<object>
    {
        public string AccommodationId { get; set; } = accommodationId;
    }
}
