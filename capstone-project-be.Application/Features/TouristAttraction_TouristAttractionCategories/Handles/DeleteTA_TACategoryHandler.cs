using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Handles
{
    public class DeleteTA_TACategoryHandler : IRequestHandler<DeleteTA_TACategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTA_TACategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTA_TACategoryRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int Id))
            {
                return new BaseResponse<CRUDTA_TACategoryDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var tA_TACategoryList = await _unitOfWork.TA_TACategoryRepository.Find(ta => ta.Id == Id);

            if (tA_TACategoryList.Count() == 0) return new BaseResponse<CRUDTA_TACategoryDTO>()
            {
                IsSuccess = false,
                Message = "Không tìm thấy mục cần xóa"
            };

            var tA_TACategory = tA_TACategoryList.First();

            await _unitOfWork.TA_TACategoryRepository.Delete(tA_TACategory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDTA_TACategoryDTO>()
            {
                IsSuccess = true,
                Message = "Xóa thành công"
            };
        }
    }
}
