using AutoMapper;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Handles
{
    public class UpdateCityHandler : IRequestHandler<UpdateCityRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateCityRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.CityId, out int cityId))
            {
                return new BaseResponse<CityDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var city = _mapper.Map<City>(request.CityData);
            city.CityId = cityId;

            await _unitOfWork.CityRepository.Update(city);
            await _unitOfWork.Save();

            return new BaseResponse<CityDTO>()
            {
                IsSuccess = true,
                Message = "Update thành phố thành công"
            };
        }
    }
}
