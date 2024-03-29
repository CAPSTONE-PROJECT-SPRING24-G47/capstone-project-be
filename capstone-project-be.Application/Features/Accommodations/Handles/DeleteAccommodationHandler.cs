﻿using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class DeleteAccommodationHandler : IRequestHandler<DeleteAccommodationRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccommodationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteAccommodationRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationId, out int accommodationId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodationList = await _unitOfWork.AccommodationRepository.Find(acc => acc.AccommodationId == accommodationId);

            if (accommodationList.Count() == 0) return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy nơi ở với Id: {accommodationId}"
            };

            var accommodation = accommodationList.First();
            var accommodationPhotos = await _unitOfWork.AccommodationPhotoRepository.
                Find(ap => ap.AccommodationId == accommodationId);
            await _unitOfWork.AccommodationPhotoRepository.DeleteRange(accommodationPhotos);

            var acc_AccCategories = await _unitOfWork.Acc_AccCategoryRepository.
                Find(acc => acc.AccommodationId == accommodationId);
            await _unitOfWork.Acc_AccCategoryRepository.DeleteRange(acc_AccCategories);

            await _unitOfWork.AccommodationRepository.Delete(accommodation);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Xóa nơi ở thành công"
            };
        }
    }
}
