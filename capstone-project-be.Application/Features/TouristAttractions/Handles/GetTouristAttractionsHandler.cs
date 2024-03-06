using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetTouristAttractionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TouristAttractionDTO>> Handle(GetTouristAttractionsRequest request, CancellationToken cancellationToken)
        {
            var touristAttractions = await _unitOfWork.TouristAttractionRepository.GetAll();

            foreach (var item in touristAttractions)
            {
                var touristAttractionId = item.TouristAttractionId;
                var touristAttractionPhotoList = await _unitOfWork.TouristAttractionPhotoRepository.
                Find(tap => tap.TouristAttractionId == touristAttractionId);
                item.TouristAttractionPhotos = touristAttractionPhotoList;
                var tA_TACategoryList = await _unitOfWork.TA_TACategoryRepository.
                    Find(tac => tac.TouristAttractionId == touristAttractionId);
                item.TouristAttraction_TouristAttractionCategories = tA_TACategoryList;
            }
            return _mapper.Map<IEnumerable<TouristAttractionDTO>>(touristAttractions);
        }

    }
}
