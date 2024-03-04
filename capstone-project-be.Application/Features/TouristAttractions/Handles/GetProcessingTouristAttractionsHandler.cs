using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class GetProcessingTouristAttractionsHandler : IRequestHandler<GetProcessingTouristAttractionsRequest, IEnumerable<TouristAttractionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProcessingTouristAttractionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TouristAttractionDTO>> Handle(GetProcessingTouristAttractionsRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.Find(ta => ta.Status.Trim().ToLower() == "Processing".Trim().ToLower());

            return _mapper.Map<IEnumerable<TouristAttractionDTO>>(touristAttractionList);
        }
    }
}
