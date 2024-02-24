using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class ApproveCreateTouristAttractionRequest(string id, string action) : IRequest<object>
    {
        public string Id { get; set; } = id;
        public string Action { get; set; } = action;
    }
}
