using AutoMapper;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Handles
{
    public class GetCityHandler : IRequestHandler<GetCityRequest, BaseResponse<CityDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCityHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CityDTO>> Handle(GetCityRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.CityId, out int cityId))
            {
                return new BaseResponse<CityDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var city = await _unitOfWork.CityRepository.GetByID(cityId);

            return new BaseResponse<CityDTO>()
            {
                IsSuccess = true,
                Data = new List<CityDTO> { _mapper.Map<CityDTO>(city) }
            };
        }
    }
}
