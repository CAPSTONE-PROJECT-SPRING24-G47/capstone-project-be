using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
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

            return _mapper.Map<IEnumerable<AccommodationDTO>>(accommodations);
        }
    }
}
