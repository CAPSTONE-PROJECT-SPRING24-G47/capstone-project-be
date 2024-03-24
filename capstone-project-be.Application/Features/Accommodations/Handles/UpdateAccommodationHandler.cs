using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.AccommodationPhotos;
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
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateAccommodationHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
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

            var accommodationData = request.UpdateAccommodationData;
            var accommodation = _mapper.Map<Accommodation>(accommodationData);
            accommodation.AccommodationId = accommodationId;
            accommodation.CreatedAt = DateTime.Now;

            var cityId = accommodation.CityId;
            var cityList = await _unitOfWork.CityRepository.Find(c => c.CityId == cityId);
            if (!cityList.Any())
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại thành phố với CityId : {cityId}"
                };
            }

            var acc_AccCategoryList = await _unitOfWork.Acc_AccCategoryRepository.Find(acc => acc.AccommodationId == accommodationId);
            await _unitOfWork.Acc_AccCategoryRepository.DeleteRange(acc_AccCategoryList);
            var acc_AccCategoryData = accommodationData.Acc_AccCategories;
            if (acc_AccCategoryData != null)
            {
                string[] parts = acc_AccCategoryData.Split(',');
                var acc_AccCategories = new List<CRUDAcc_AccCategoryDTO>();
                foreach (string part in parts)
                {
                    acc_AccCategories.Add(
                        new CRUDAcc_AccCategoryDTO
                        {
                            AccommodationId = accommodationId,
                            AccommodationCategoryId = int.Parse(part)
                        });
                }
                acc_AccCategoryList = _mapper.Map<IEnumerable<Accommodation_AccommodationCategory>>(acc_AccCategories.Distinct());
                await _unitOfWork.Acc_AccCategoryRepository.AddRange(acc_AccCategoryList);
            }

            var deletePhotos = accommodationData.DeletePhotos;
            if (deletePhotos != null)
            {
                var deletePhotoIds = new List<int>();
                var parts = deletePhotos.Split(",");
                foreach (string part in parts)
                {
                    deletePhotoIds.Add(int.Parse(part));
                }
                var accommodationPhotoList = await _unitOfWork.AccommodationPhotoRepository.
                    Find(ap => ap.AccommodationId == accommodationId && deletePhotoIds.Contains(ap.AccommodationPhotoId));

                foreach (var ac in accommodationPhotoList)
                {
                    await _storageRepository.DeleteFileAsync(ac.SavedFileName);
                }
                await _unitOfWork.AccommodationPhotoRepository.DeleteRange(accommodationPhotoList);
            }

            var photoData = accommodationData.Photos;
            if (photoData != null)
            {
                var accommodationPhotos = new List<CRUDAccommodationPhotoDTO>();
                foreach (var photo in photoData)
                {
                    if (photo != null)
                    {
                        var savedFileName = GenerateFileNameToSave(photo.FileName);
                        accommodationPhotos.Add(
                            new CRUDAccommodationPhotoDTO
                            {
                                AccommodationId = accommodationId,
                                PhotoURL = await _storageRepository.UpLoadFileAsync(photo, savedFileName),
                                SavedFileName = savedFileName
                            }
                            );
                    }
                }
                var accommodationPhotoList = _mapper.Map<IEnumerable<AccommodationPhoto>>(accommodationPhotos);
                await _unitOfWork.AccommodationPhotoRepository.AddRange(accommodationPhotoList);
            }

            await _unitOfWork.AccommodationRepository.Update(accommodation);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Update nơi ở thành công"
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
