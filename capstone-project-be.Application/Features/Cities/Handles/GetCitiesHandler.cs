using AutoMapper;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Handles
{
    public class GetCitiesHandler : IRequestHandler<GetCitiesRequest, IEnumerable<CityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCitiesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> Handle(GetCitiesRequest request, CancellationToken cancellationToken)
        {
            var cities = await _unitOfWork.CityRepository.GetAll();

            return _mapper.Map<IEnumerable<CityDTO>>(cities);
        }
    }
}
