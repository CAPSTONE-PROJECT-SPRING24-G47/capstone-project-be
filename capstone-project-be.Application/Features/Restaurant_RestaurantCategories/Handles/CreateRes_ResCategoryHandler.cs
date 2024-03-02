using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Handles
{
    public class CreateRes_ResCategoryHandler : IRequestHandler<CreateRes_ResCategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRes_ResCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateRes_ResCategoryRequest request, CancellationToken cancellationToken)
        {
            var res_ResCategoryData = request.Res_ResCategoryData;
            var res_resCatagory = _mapper.Map<Restaurant_RestaurantCategory>(res_ResCategoryData);
            var restaurantList = await _unitOfWork.RestaurantRepository.
                Find(r => r.RestaurantId == res_ResCategoryData.RestaurantId);
            if (!restaurantList.Any())
            {
                return new BaseResponse<CRUDRes_ResCategoryDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại nhà hàng với ID = {res_resCatagory.RestaurantId}"
                };
            }
            var restaurantCategoryList = await _unitOfWork.RestaurantCategoryRepository.
                Find(rc => rc.RestaurantCategoryId == res_ResCategoryData.RestaurantCategoryId);
            if (!restaurantCategoryList.Any())
            {
                return new BaseResponse<CRUDRes_ResCategoryDTO>()
                {
                    IsSuccess = false,
                    Message = "Thêm thất bại"
                };
            }

            await _unitOfWork.Res_ResCategoryRepository.Add(res_resCatagory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDRes_ResCategoryDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }
    }
}
