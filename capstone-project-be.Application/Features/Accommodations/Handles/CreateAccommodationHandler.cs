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
    public class CreateAccommodationHandler : IRequestHandler<CreateAccommodationRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateAccommodationHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
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
            }
            else accommodation.Status = "Processing";
            accommodation.CreatedAt = DateTime.Now;
            string[] ranges = accommodation.PriceRange.Split('-');
            var min = int.Parse(ranges[0]);
            var max = int.Parse(ranges[1]);
            var average = (min + max) / 2;
            if (average < 2000000) accommodation.PriceLevel = "Giá thấp";
            else if (average < 4000000) accommodation.PriceLevel = "Trung bình";
            else accommodation.PriceLevel = "Giá cao";

            await _unitOfWork.AccommodationRepository.Add(accommodation);
            await _unitOfWork.Save();

            var accommodationList = await _unitOfWork.AccommodationRepository.
                Find(a => a.UserId == accommodation.UserId && a.CreatedAt >= DateTime.Now.AddMinutes(-1));
            var accommodationId = accommodationList.First().AccommodationId;
            var acc_AccCategoryData = accommodationData.Acc_AccCategories;
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
            var acc_AccCategoryList = _mapper.Map<IEnumerable<Accommodation_AccommodationCategory>>(acc_AccCategories.Distinct());
            await _unitOfWork.Acc_AccCategoryRepository.AddRange(acc_AccCategoryList);

            var photoData = accommodationData.Photos;
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
            await _unitOfWork.Save();
            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nơi ở mới thành công"
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
