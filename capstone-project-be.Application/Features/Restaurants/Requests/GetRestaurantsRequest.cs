﻿using capstone_project_be.Application.DTOs.Restaurants;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class GetRestaurantsRequest : IRequest<IEnumerable<RestaurantDTO>>
    {
    }
}
