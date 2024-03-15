using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Requests
{
    public class DeleteTrip_AccommodationRequest(string Id) : IRequest<object>
    {
        public string Id { get; set; } = Id;
    }
}
