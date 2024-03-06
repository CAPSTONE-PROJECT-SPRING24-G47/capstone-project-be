using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class DeleteTouristAttractionCommentHandler : IRequestHandler<DeleteTouristAttractionCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTouristAttractionCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTouristAttractionCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionCommentId, out int TouristAttractionCommentId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var TouristAttractionCommentList = await _unitOfWork.TouristAttractionCommentRepository.
                Find(acc => acc.TouristAttractionCommentId == TouristAttractionCommentId);

            if (TouristAttractionCommentList.Count() == 0) return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy comment với Id: {TouristAttractionCommentId}"
            };

            var TouristAttractionComment = TouristAttractionCommentList.First();

            await _unitOfWork.TouristAttractionCommentRepository.Delete(TouristAttractionComment);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Message = "Xóa comment thành công"
            };
        }
    }
}
