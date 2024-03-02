using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Handles
{
    public class DeleteRes_ResCategoryHandler : IRequestHandler<DeleteRes_ResCategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRes_ResCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteRes_ResCategoryRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int Id))
            {
                return new BaseResponse<CRUDRes_ResCategoryDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var res_ResCategoryList = await _unitOfWork.Res_ResCategoryRepository.Find(r => r.Id == Id);

            if (res_ResCategoryList.Count() == 0) return new BaseResponse<CRUDRes_ResCategoryDTO>()
            {
                IsSuccess = false,
                Message = "Không tìm thấy mục cần xóa"
            };

            var res_ResCategory = res_ResCategoryList.First();

            await _unitOfWork.Res_ResCategoryRepository.Delete(res_ResCategory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDRes_ResCategoryDTO>()
            {
                IsSuccess = true,
                Message = "Xóa thành công"
            };
        }
    }
}
