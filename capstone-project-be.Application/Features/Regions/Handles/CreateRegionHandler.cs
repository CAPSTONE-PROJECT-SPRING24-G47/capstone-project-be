using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Regions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Handles
{
    public class CreateRegionHandler : IRequestHandler<CreateRegionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRegionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateRegionRequest request, CancellationToken cancellationToken)
        {
            var regionData = request.RegionData;
            var region = _mapper.Map<Region>(regionData);

            await _unitOfWork.RegionRepository.Add(region);
            await _unitOfWork.Save();

            return new BaseResponse<RegionDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công vùng mới"
            };
        }
    }
}
