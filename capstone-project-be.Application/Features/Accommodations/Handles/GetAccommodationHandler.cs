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
    public class GetAccommodationHandler : IRequestHandler<GetAccommodationRequest, BaseResponse<AccommodationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetAccommodationHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccommodationDTO>> Handle(GetAccommodationRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationId, out int accommodationId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodation = _mapper.Map<AccommodationDTO>
                (await _unitOfWork.AccommodationRepository.GetByID(accommodationId));
            
            if (accommodation == null)
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy nơi ở!"
                };
            }

            var accommodationPhotoList = _mapper.Map <IEnumerable<AccommodationPhotoDTO>>
                (await _unitOfWork.AccommodationPhotoRepository.
                Find(ap => ap.AccommodationId == accommodationId));

            foreach (var item in accommodationPhotoList)
            {
                item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
            }

            accommodation.AccommodationPhotos = accommodationPhotoList;
            var acc_accCategoryList = _mapper.Map<IEnumerable<CRUDAcc_AccCategoryDTO>>
                (await _unitOfWork.Acc_AccCategoryRepository.
                Find(acc => acc.AccommodationId == accommodationId));
            accommodation.Accommodation_AccommodationCategories = acc_accCategoryList;

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Data = new List<AccommodationDTO> { accommodation }
            };
        }
    }
}
