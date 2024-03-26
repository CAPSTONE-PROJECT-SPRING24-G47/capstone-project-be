using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.AccommodationPhotos;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class GetAccommodationsHandler : IRequestHandler<GetAccommodationsRequest, IEnumerable<AccommodationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetAccommodationsHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccommodationDTO>> Handle(GetAccommodationsRequest request, CancellationToken cancellationToken)
        {
            var accommodations = _mapper.Map<IEnumerable<AccommodationDTO>>
                (await _unitOfWork.AccommodationRepository.GetAll());

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            accommodations = accommodations.Skip(skip).Take(pageSize);

            foreach (var item in accommodations)
            {
                var accommodationPhotoList = _mapper.Map<IEnumerable<AccommodationPhotoDTO>>
                (await _unitOfWork.AccommodationPhotoRepository.
                Find(ap => ap.AccommodationId == item.AccommodationId));

                foreach (var photo in accommodationPhotoList)
                {
                    photo.SignedUrl = await _storageRepository.GetSignedUrlAsync(photo.SavedFileName);
                }

                item.AccommodationPhotos = accommodationPhotoList;

                var acc_accCategoryList = _mapper.Map<IEnumerable<CRUDAcc_AccCategoryDTO>>
                    (await _unitOfWork.Acc_AccCategoryRepository.
                    Find(acc => acc.AccommodationId == item.AccommodationId));
                item.Accommodation_AccommodationCategories = acc_accCategoryList;
            }

            return _mapper.Map<IEnumerable<AccommodationDTO>>(accommodations);
        }
    }
}
