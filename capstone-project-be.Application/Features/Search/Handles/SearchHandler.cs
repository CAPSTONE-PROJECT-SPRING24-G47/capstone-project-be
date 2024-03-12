using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.Features.Search.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Search.Handles
{
    public class SearchHandler : IRequestHandler<SearchRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(SearchRequest request, CancellationToken cancellationToken)
        {
            string value = request.SearchData.Value;
            string type = request.SearchData.Type;
            string property = request.SearchData.Property;

            IEnumerable<dynamic> list = Enumerable.Empty<dynamic>();

            switch (type)
            {
                case "Users":
                    list = await _unitOfWork.UserRepository.FindValueContain(property.Trim(), value.Trim());
                    break;
                case "Regions":
                    list = await _unitOfWork.RegionRepository.FindValueContain(property.Trim(), value.Trim());
                    break;
                case "Prefectures":
                    list = await _unitOfWork.PrefectureRepository.FindValueContain(property.Trim(), value.Trim());
                    break;
                case "Cities":
                    list = await _unitOfWork.CityRepository.FindValueContain(property.Trim(), value.Trim());
                    break;
                case "Restaurants":
                    list = await _unitOfWork.RestaurantRepository.FindValueContain(property.Trim(), value.Trim());
                    break;
                case "Accommodations":
                    list = await _unitOfWork.AccommodationRepository.FindValueContain(property.Trim(), value.Trim());
                    break;
                case "TouristAttractions":
                    list = await _unitOfWork.TouristAttractionRepository.FindValueContain(property.Trim(), value.Trim());
                    break;
                default:
                    return new BaseResponse<AccommodationDTO>()
                    {
                        IsSuccess = false,
                        Message = $"Không tìm thấy type: {type}"
                    };

            }

            if (list.Any())
            {
                return list;
            }

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy kết quả có {property} là {value}"
            };
        }
    }
}