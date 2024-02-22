using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
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
            var touristAttraction = await _unitOfWork.TouristAttractionRepository.GetAll();

            return _mapper.Map<IEnumerable<TouristAttractionDTO>>(touristAttraction);
        }

    }
}
