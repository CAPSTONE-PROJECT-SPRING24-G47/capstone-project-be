using AutoMapper;
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
    public class GetTouristAttractionHandler : IRequestHandler<GetTouristAttractionRequest, BaseResponse<TouristAttractionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetTouristAttractionHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<TouristAttractionDTO>> Handle(GetTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionId, out int touristAttractionId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttraction = _mapper.Map<TouristAttractionDTO>
                (await _unitOfWork.TouristAttractionRepository.GetByID(touristAttractionId));
            if (touristAttraction == null)
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy địa điểm giải trí!"
                };
            }

            var touristAttractionPhotoList = _mapper.Map<IEnumerable<TouristAttractionPhotoDTO>>
                (await _unitOfWork.TouristAttractionPhotoRepository.
                Find(tap => tap.TouristAttractionId == touristAttractionId));

            foreach (var item in touristAttractionPhotoList)
            {
                item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
            }

            touristAttraction.TouristAttractionPhotos = touristAttractionPhotoList;
            var ta_taCategoryList = _mapper.Map<IEnumerable<CRUDTA_TACategoryDTO>>
                (await _unitOfWork.TA_TACategoryRepository.
                Find(ta => ta.TouristAttractionId == touristAttractionId));
            touristAttraction.TouristAttraction_TouristAttractionCategories = ta_taCategoryList;

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Data = new List<TouristAttractionDTO> { touristAttraction }
            };
        }
    }
}
