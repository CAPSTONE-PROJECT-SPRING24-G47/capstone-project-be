using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Regions.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Handles
{
    public class GetRegionsHandler : IRequestHandler<GetRegionsRequest, IEnumerable<RegionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRegionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RegionDTO>> Handle(GetRegionsRequest request, CancellationToken cancellationToken)
        {
            var regions = await _unitOfWork.RegionRepository.GetAll();

            return _mapper.Map<IEnumerable<RegionDTO>>(regions);
        }
    }
}
