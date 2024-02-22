using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Handles
{
    public class DeleteCityHandler : IRequestHandler<DeleteCityRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteCityRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.CityId, out int cityId))
            {
                return new BaseResponse<CityDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var cityList = await _unitOfWork.CityRepository.Find(city => city.CityId == cityId);

            if (cityList.Count() == 0) return new BaseResponse<CityDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy thành phố với id là {cityId}"
            };

            var city = cityList.First();

            await _unitOfWork.CityRepository.Delete(city);
            await _unitOfWork.Save();

            return new BaseResponse<CityDTO>()
            {
                IsSuccess = true,
                Message = "Xóa thành phố thành công"
            };
        }
    }
}
