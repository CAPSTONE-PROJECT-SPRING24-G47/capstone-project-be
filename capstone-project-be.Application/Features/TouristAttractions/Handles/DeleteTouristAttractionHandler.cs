using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class DeleteTouristAttractionHandler : IRequestHandler<DeleteTouristAttractionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTouristAttractionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionId, out int touristAttractionId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.Find(ta => ta.TouristAttractionId == touristAttractionId);

            if (touristAttractionList.Count() == 0) return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy nhà hàng với Id: {touristAttractionId}"
            };

            var touristAttraction = touristAttractionList.First();

            await _unitOfWork.TouristAttractionRepository.Delete(touristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Xóa địa điểm du lịch thành công"
            };
        }
    }
}
