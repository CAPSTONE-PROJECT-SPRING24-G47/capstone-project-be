﻿using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class DeleteAccommodationCommentRequest(string accommodationCommentId) : IRequest<object>
    {
        public string AccommodationCommentId { get; set; } = accommodationCommentId;
    }
}
