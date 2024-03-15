using MediatR;

namespace capstone_project_be.Application.Features.Trip_TouristAttractions.Requests
{
    public class DeleteTrip_TouristAttractionRequest(string Id) : IRequest<object>
    {
        public string Id { get; set; } = Id;
    }
}
