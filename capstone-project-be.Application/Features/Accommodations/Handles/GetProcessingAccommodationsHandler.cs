﻿using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class GetProcessingAccommodationsHandler : IRequestHandler<GetProcessingAccommodationsRequest, IEnumerable<AccommodationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProcessingAccommodationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccommodationDTO>> Handle(GetProcessingAccommodationsRequest request, CancellationToken cancellationToken)
        {
            var accommodationList = await _unitOfWork.AccommodationRepository.Find(a => a.Status == "Processing");

            return _mapper.Map<IEnumerable<AccommodationDTO>>(accommodationList);
        }
    }
}