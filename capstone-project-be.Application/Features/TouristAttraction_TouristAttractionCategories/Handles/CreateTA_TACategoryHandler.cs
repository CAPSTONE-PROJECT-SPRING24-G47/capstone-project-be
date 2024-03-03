using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Handles
{
    public class CreateTA_TACategoryHandler : IRequestHandler<CreateTA_TACategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTA_TACategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateTA_TACategoryRequest request, CancellationToken cancellationToken)
        {
            var tA_TACategoryData = request.TA_TACategoryData;
            var tA_TACatagory = _mapper.Map<TouristAttraction_TouristAttractionCategory>(tA_TACategoryData);
            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.
                Find(ta => ta.TouristAttractionId == tA_TACategoryData.TouristAttractionId);
            if (!touristAttractionList.Any())
            {
                return new BaseResponse<CRUDTA_TACategoryDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại địa điểm giải trí với ID = {tA_TACatagory.TouristAttractionId}"
                };
            }
            var touristAttractionCategoryList = await _unitOfWork.TouristAttractionCategoryRepository.
                Find(tac => tac.TouristAttractionCategoryId == tA_TACategoryData.TouristAttractionCategoryId);
            if (!touristAttractionList.Any())
            {
                return new BaseResponse<CRUDTA_TACategoryDTO>()
                {
                    IsSuccess = false,
                    Message = "Thêm thất bại"
                };
            }

            await _unitOfWork.TA_TACategoryRepository.Add(tA_TACatagory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDTA_TACategoryDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }
    }
}
