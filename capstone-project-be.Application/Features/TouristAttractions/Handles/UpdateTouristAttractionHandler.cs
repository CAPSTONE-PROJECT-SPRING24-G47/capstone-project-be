using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class UpdateTouristAttractionHandler : IRequestHandler<UpdateTouristAttractionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTouristAttractionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionId, out int touristAttractionId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttraction = _mapper.Map<TouristAttraction>(request.TouristAttractionData);
            touristAttraction.TouristAttractionId = touristAttractionId;

            var cityId = touristAttraction.CityId;
            var cityList = await _unitOfWork.CityRepository.Find(c => c.CityId == cityId);
            if (!cityList.Any())
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại thành phố với CityId : {cityId}"
                };
            }

            var tA_TACategoryList = await _unitOfWork.TA_TACategoryRepository.
                Find(ta => ta.TouristAttractionId == touristAttractionId);
            await _unitOfWork.TA_TACategoryRepository.DeleteRange(tA_TACategoryList);
            tA_TACategoryList = touristAttraction.TouristAttraction_TouristAttractionCategories.ToList();
            await _unitOfWork.TA_TACategoryRepository.AddRange(tA_TACategoryList);

            var touristAttractionPhotoList = await _unitOfWork.TouristAttractionPhotoRepository.
                Find(ta => ta.TouristAttractionId == touristAttractionId);
            await _unitOfWork.TouristAttractionPhotoRepository.DeleteRange(touristAttractionPhotoList);
            touristAttractionPhotoList = touristAttraction.TouristAttractionPhotos.ToList();
            await _unitOfWork.TouristAttractionPhotoRepository.AddRange(touristAttractionPhotoList);

            await _unitOfWork.TouristAttractionRepository.Update(touristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Update địa điểm du lịch giải trí thành công"
            };
        }
    }
}
