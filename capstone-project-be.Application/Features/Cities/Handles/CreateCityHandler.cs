using AutoMapper;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.CIties.Handles
{
    public class CreateCityHandler : IRequestHandler<CreateCityRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateCityRequest request, CancellationToken cancellationToken)
        {
            var cityData = request.CityData;
            var city = _mapper.Map<City>(cityData);

            await _unitOfWork.CityRepository.Add(city);
            await _unitOfWork.Save();

            return new BaseResponse<CityDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công thành phố mới"
            };
        }
    }
}
