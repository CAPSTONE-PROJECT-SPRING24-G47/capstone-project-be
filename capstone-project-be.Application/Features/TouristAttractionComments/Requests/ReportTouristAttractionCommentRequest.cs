﻿using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class ReportTouristAttractionCommentRequest(string touristAttractionCommentId) : IRequest<object>
    {
        public string TouristAttractionCommentId { get; set; } = touristAttractionCommentId;
    }
}
