﻿using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class GetCommentsByAccommodationIdRequest(string accommodationId, int pageIndex) : IRequest<BaseResponse<AccommodationCommentDTO>>
    {
        public string AccommodationId { get; set; } = accommodationId;
        public int PageIndex { get; set; } = pageIndex;
    }
}
