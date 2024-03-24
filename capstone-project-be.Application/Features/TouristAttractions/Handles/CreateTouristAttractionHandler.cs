using AutoMapper;
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
    public class CreateTouristAttractionHandler : IRequestHandler<CreateTouristAttractionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateTouristAttractionHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionData = request.TouristAttractionData;
            var touristAttraction = _mapper.Map<TouristAttraction>(touristAttractionData);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == touristAttraction.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {touristAttraction.UserId}"
                };
            }
            var user = userList.First();
            if (user.RoleId == 3)
            {
                touristAttraction.Status = "Approved";
                touristAttraction.CreatedAt = DateTime.Now;
            }
            else touristAttraction.Status = "Processing";
            touristAttraction.CreatedAt = DateTime.Now;
            await _unitOfWork.TouristAttractionRepository.Add(touristAttraction);
            await _unitOfWork.Save();

            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.
                Find(ta => ta.UserId == touristAttraction.UserId && ta.CreatedAt >= DateTime.Now.AddMinutes(-1));
            var touristAttractionId = touristAttractionList.First().TouristAttractionId;
            var ta_TACategoryData = touristAttractionData.TA_TACategories;
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
            var ta_TACategoryList = _mapper.Map<IEnumerable<TouristAttraction_TouristAttractionCategory>>(ta_TACategories.Distinct());
            await _unitOfWork.TA_TACategoryRepository.AddRange(ta_TACategoryList);

            var photoData = touristAttractionData.Photos;
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
            var touristAttractionPhotoList = _mapper.Map<IEnumerable<TouristAttractionPhoto>>(touristAttractionPhotos);
            await _unitOfWork.TouristAttractionPhotoRepository.AddRange(touristAttractionPhotoList);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Thêm địa điểm giải trí mới thành công"
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
