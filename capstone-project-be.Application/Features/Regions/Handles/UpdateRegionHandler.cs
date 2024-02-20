using AutoMapper;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.Features.Regions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Handles
{
    public class UpdateRegionHandler : IRequestHandler<UpdateRegionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRegionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateRegionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RegionId, out int regionId))
            {
                return new BaseResponse<RegionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var region = _mapper.Map<Region>(request.RegionData);
            region.RegionId = regionId;

            await _unitOfWork.RegionRepository.Update(region);
            await _unitOfWork.Save();

            return new BaseResponse<RegionDTO>()
            {
                IsSuccess = true,
                Message = "Update vùng thành công"
            };
        }
    }
}
