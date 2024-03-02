using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests
{
    public class CreateRes_ResCategoryRequest(CRUDRes_ResCategoryDTO res_resCategoryData) : IRequest<object>
    {
        public CRUDRes_ResCategoryDTO Res_ResCategoryData { get; set; } = res_resCategoryData;
    }
}
