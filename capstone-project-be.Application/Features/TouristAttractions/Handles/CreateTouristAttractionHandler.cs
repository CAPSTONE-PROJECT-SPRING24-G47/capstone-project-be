﻿using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class CreateTouristAttractionHandler : IRequestHandler<CreateTouristAttractionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTouristAttractionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionData = request.TouristAttractionData;
            var touristAttraction = _mapper.Map<TouristAttraction>(touristAttractionData);

            await _unitOfWork.TouristAttractionRepository.Add(touristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Thêm địa điểm du lịch mới thành công"
            };
        }
    }
}