using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.Features.Regions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Handles
{
    public class DeleteRegionHandler : IRequestHandler<DeleteRegionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRegionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteRegionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RegionId, out int regionId))
            {
                return new BaseResponse<RegionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var regionList = await _unitOfWork.RegionRepository.Find(region => region.RegionId == regionId);

            if (regionList.Count() == 0) return new BaseResponse<RegionDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy vùng với id là {regionId}"
            };

            var region = regionList.First();

            await _unitOfWork.RegionRepository.Delete(region);
            await _unitOfWork.Save();

            return new BaseResponse<RegionDTO>()
            {
                IsSuccess = true,
                Message = "Xóa vùng thành công"
            };
        }
    }
}
