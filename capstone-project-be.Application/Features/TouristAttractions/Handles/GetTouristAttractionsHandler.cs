using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.DTOs.TouristAttractionPhotos;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class GetTouristAttractionsHandler : IRequestHandler<GetTouristAttractionsRequest, IEnumerable<TouristAttractionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetTouristAttractionsHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TouristAttractionDTO>> Handle(GetTouristAttractionsRequest request, CancellationToken cancellationToken)
        {
            var touristAttractions = _mapper.Map<IEnumerable<TouristAttractionDTO>>
                (await _unitOfWork.TouristAttractionRepository.GetAll());

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            touristAttractions = touristAttractions.Skip(skip).Take(pageSize);

            foreach (var item in touristAttractions)
            {
                var touristAttractionPhotoList = _mapper.Map<IEnumerable<TouristAttractionPhotoDTO>>
                (await _unitOfWork.TouristAttractionPhotoRepository.
                Find(tap => tap.TouristAttractionId == item.TouristAttractionId));

                foreach (var photo in touristAttractionPhotoList)
                {
                    photo.SignedUrl = await _storageRepository.GetSignedUrlAsync(photo.SavedFileName);
                }

                item.TouristAttractionPhotos = touristAttractionPhotoList;

                var ta_taCategoryList = _mapper.Map<IEnumerable<CRUDTA_TACategoryDTO>>
                    (await _unitOfWork.TA_TACategoryRepository.
                    Find(ta => ta.TouristAttractionId == item.TouristAttractionId));
                item.TouristAttraction_TouristAttractionCategories = ta_taCategoryList;
            }

            return _mapper.Map<IEnumerable<TouristAttractionDTO>>(touristAttractions);
        }
    }

}

