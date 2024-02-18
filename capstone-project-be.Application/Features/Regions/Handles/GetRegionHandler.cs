using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Regions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Handles
{
    public class GetRegionHandler : IRequestHandler<GetRegionRequest, BaseResponse<RegionDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRegionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<RegionDTO>> Handle(GetRegionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RegionId, out int regionId))
            {
                return new BaseResponse<RegionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var region = await _unitOfWork.RegionRepository.GetByID(regionId);

            return new BaseResponse<RegionDTO>()
            {
                IsSuccess = true,
                Data = new List<RegionDTO> { _mapper.Map<RegionDTO>(region) }
            };
        }
    }
}

