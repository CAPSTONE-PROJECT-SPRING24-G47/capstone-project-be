using AutoMapper;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class GetTouristAttractionNumberHandler : IRequestHandler<GetTouristAttractionNumberRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTouristAttractionNumberHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetTouristAttractionNumberRequest request, CancellationToken cancellationToken)
        {
            var touristAttraction = await _unitOfWork.TouristAttractionRepository.GetAll();

            var result = touristAttraction.ToList().Count();

            return result;
        }
    }
}
