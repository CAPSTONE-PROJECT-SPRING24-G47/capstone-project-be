using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class DeleteAccommodationCommentHandler : IRequestHandler<DeleteAccommodationCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccommodationCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteAccommodationCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationCommentId, out int accommodationCommentId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodationCommentList = await _unitOfWork.AccommodationCommentRepository.
                Find(acc => acc.AccommodationCommentId == accommodationCommentId);

            if (accommodationCommentList.Count() == 0) return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy comment với Id: {accommodationCommentId}"
            };

            var accommodationComment = accommodationCommentList.First();

            await _unitOfWork.AccommodationCommentRepository.Delete(accommodationComment);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Message = "Xóa comment thành công"
            };
        }
    }
}
