﻿using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;
using System.Xml;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class CreateAccommodationHandler : IRequestHandler<CreateAccommodationRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAccommodationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateAccommodationRequest request, CancellationToken cancellationToken)
        {
            var accommodationData = request.AccommodationData;
            var accommodation = _mapper.Map<Accommodation>(accommodationData);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == accommodation.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {accommodation.UserId}"
                };
            }

            var user = userList.First();
            if (user.RoleId == 3)
            {
                accommodation.Status = "Approved";
                accommodation.CreatedAt = DateTime.Now;
            }
            else accommodation.Status = "Processing";
            await _unitOfWork.AccommodationRepository.Add(accommodation);

            var accommodationId = accommodation.AccommodationId;
            var acc_AccCategories = accommodationData.Accommodation_AccommodationCategories;
            var acc_AccCategoryList = _mapper.Map<IEnumerable<Accommodation_AccommodationCategory>>(acc_AccCategories);
            foreach (var item in acc_AccCategoryList)
            {
                item.AccommodationId = accommodationId;
            }
            await _unitOfWork.Acc_AccCategoryRepository.AddRange(acc_AccCategoryList);

            await _unitOfWork.Save();

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nơi ở mới thành công"
            };
        }
    }
}
