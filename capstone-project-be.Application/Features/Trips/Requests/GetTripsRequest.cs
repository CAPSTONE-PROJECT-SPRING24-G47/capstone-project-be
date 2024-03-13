using capstone_project_be.Application.DTOs.Trips;   
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Requests
{
    public class GetTripsRequest : IRequest<IEnumerable<TripDTO>>
    {

    }
}
