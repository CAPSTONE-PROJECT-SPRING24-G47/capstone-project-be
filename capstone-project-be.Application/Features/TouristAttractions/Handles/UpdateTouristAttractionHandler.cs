using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantPhotos;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.DTOs.TouristAttractionPhotos;
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
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateTouristAttractionHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
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

            var touristAttractionData = request.UpdateTouristAttractionData;
            var touristAttraction = _mapper.Map<TouristAttraction>(touristAttractionData);
            touristAttraction.TouristAttractionId = touristAttractionId;
            touristAttraction.CreatedAt = DateTime.Now;

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

            var ta_TACategoryList = await _unitOfWork.TA_TACategoryRepository.Find(ta => ta.TouristAttractionId == touristAttractionId);
            await _unitOfWork.TA_TACategoryRepository.DeleteRange(ta_TACategoryList);
            var ta_TACategoryData = touristAttractionData.TA_TACategories;
            if (ta_TACategoryData != null)
            {
                string[] parts = ta_TACategoryData.Split(',');
                var ta_TACategories = new List<CRUDTA_TACategoryDTO>();
                foreach (string part in parts)
                {
                    ta_TACategories.Add(
                        new CRUDTA_TACategoryDTO
                        {
                            TouristAttractionId = touristAttractionId,
                            TouristAttractionCategoryId = int.Parse(part)
                        });
                }
                ta_TACategoryList = _mapper.Map<IEnumerable<TouristAttraction_TouristAttractionCategory>>(ta_TACategories.Distinct());
                await _unitOfWork.TA_TACategoryRepository.AddRange(ta_TACategoryList);
            }

            var deletePhotos = touristAttractionData.DeletePhotos;
            if (deletePhotos != null)
            {
                var deletePhotoIds = new List<int>();
                var parts = deletePhotos.Split(",");
                foreach (string part in parts)
                {
                    deletePhotoIds.Add(int.Parse(part));
                }
                var touristAttractionPhotoList = await _unitOfWork.TouristAttractionPhotoRepository.
                    Find(tap => tap.TouristAttractionId == touristAttractionId && deletePhotoIds.Contains(tap.TouristAttractionPhotoId));

                foreach (var tap in touristAttractionPhotoList)
                {
                    await _storageRepository.DeleteFileAsync(tap.SavedFileName);
                }
                await _unitOfWork.TouristAttractionPhotoRepository.DeleteRange(touristAttractionPhotoList);
                var photoData = touristAttractionData.Photos;
                if (photoData != null)
                {
                    var touristAttractionPhotos = new List<CRUDTouristAttractionPhotoDTO>();
                    foreach (var photo in photoData)
                    {
                        if (photo != null)
                        {
                            var savedFileName = GenerateFileNameToSave(photo.FileName);
                            touristAttractionPhotos.Add(
                                new CRUDTouristAttractionPhotoDTO
                                {
                                    TouristAttractionId = touristAttractionId,
                                    PhotoURL = await _storageRepository.UpLoadFileAsync(photo, savedFileName),
                                    SavedFileName = savedFileName
                                }
                                );
                        }
                    }
                    touristAttractionPhotoList = _mapper.Map<IEnumerable<TouristAttractionPhoto>>(touristAttractionPhotos);
                    await _unitOfWork.TouristAttractionPhotoRepository.AddRange(touristAttractionPhotoList);
                }
            }

            await _unitOfWork.TouristAttractionRepository.Update(touristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Update địa điểm du lịch giải trí thành công"
            };
        }

        private string? GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
