using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class UpdateAccommodationHandler : IRequestHandler<UpdateAccommodationRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAccommodationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateAccommodationRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationId, out int accommodationId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodation = _mapper.Map<Accommodation>(request.UpdateAccommodationData);
            accommodation.AccommodationId = accommodationId;
            accommodation.CreatedAt = DateTime.Now;

            var cityId = accommodation.CityId;
            var cityList = await _unitOfWork.CityRepository.Find(c => c.CityId == cityId);
            if(!cityList.Any()) 
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại thành phố với CityId : {cityId}"
                };
            }

            var acc_AccCategoryList = await _unitOfWork.Acc_AccCategoryRepository.Find(acc => acc.AccommodationId == accommodationId);
            await _unitOfWork.Acc_AccCategoryRepository.DeleteRange(acc_AccCategoryList);
            acc_AccCategoryList = accommodation.Accommodation_AccommodationCategories.ToList();
            await _unitOfWork.Acc_AccCategoryRepository.AddRange(acc_AccCategoryList);

            var accommodationPhotoList = await _unitOfWork.AccommodationPhotoRepository.
                Find(ap => ap.AccommodationId == accommodationId);
            await _unitOfWork.AccommodationPhotoRepository.DeleteRange(accommodationPhotoList);
            accommodationPhotoList = accommodation.AccommodationPhotos.ToList();
            await _unitOfWork.AccommodationPhotoRepository.AddRange(accommodationPhotoList);

            await _unitOfWork.AccommodationRepository.Update(accommodation);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Update nơi ở thành công"
            };
        }
    }
}
