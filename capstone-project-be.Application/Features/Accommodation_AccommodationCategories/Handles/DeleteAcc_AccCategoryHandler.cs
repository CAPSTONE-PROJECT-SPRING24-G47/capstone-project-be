using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Handles
{
    public class DeleteAcc_AccCategoryHandler : IRequestHandler<DeleteAcc_AccCategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAcc_AccCategoryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteAcc_AccCategoryRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int Id))
            {
                return new BaseResponse<CRUDAcc_AccCategoryDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var acc_AccCategoryList = await _unitOfWork.Acc_AccCategoryRepository.Find(a => a.Id == Id);

            if (acc_AccCategoryList.Count() == 0) return new BaseResponse<CRUDAcc_AccCategoryDTO>()
            {
                IsSuccess = false,
                Message = "Không tìm thấy mục cần xóa"
            };

            var acc_AccCategory = acc_AccCategoryList.First();

            await _unitOfWork.Acc_AccCategoryRepository.Delete(acc_AccCategory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDAcc_AccCategoryDTO>()
            {
                IsSuccess = true,
                Message = "Xóa thành công"
            };
        }
    }
}
