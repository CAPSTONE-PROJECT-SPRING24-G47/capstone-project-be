using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetAccommodationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccommodationDTO>> Handle(GetAccommodationsRequest request, CancellationToken cancellationToken)
        {
            var accommodations = await _unitOfWork.AccommodationRepository.GetAll();

            foreach (var item in accommodations)
            {
                var accommodationId = item.AccommodationId;
                var accommodationPhotoList = await _unitOfWork.AccommodationPhotoRepository.
                Find(ap => ap.AccommodationId == accommodationId);
                item.AccommodationPhotos = accommodationPhotoList;
                var acc_accCategoryList = await _unitOfWork.Acc_AccCategoryRepository.
                    Find(acc => acc.AccommodationId == accommodationId);
                item.Accommodation_AccommodationCategories = acc_accCategoryList;
            }

            return _mapper.Map<IEnumerable<AccommodationDTO>>(accommodations);
        }
    }
}
