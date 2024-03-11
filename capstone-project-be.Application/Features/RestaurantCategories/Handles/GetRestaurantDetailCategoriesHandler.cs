using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.RestaurantCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantCategories.Handles
{
    public class GetRestaurantDetailCategoriesHandler : IRequestHandler<GetRestaurantDetailCategoriesRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetRestaurantDetailCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(GetRestaurantDetailCategoriesRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int restaurantId))
            {
                return new BaseResponse<RestaurantCategoriesDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var list = await _unitOfWork.Res_ResCategoryRepository.GetRestaurantDetailCategories(restaurantId);

            return new BaseResponse<RestaurantCategoriesDTO>()
            {
                IsSuccess = true,
                Data = list
            };
        }
    }
}
