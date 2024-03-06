using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class ReportAccommodationCommentHandler : IRequestHandler<ReportAccommodationCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportAccommodationCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(ReportAccommodationCommentRequest request, CancellationToken cancellationToken)
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
            accommodationComment.IsReported = true;

            await _unitOfWork.AccommodationCommentRepository.Update(accommodationComment);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Message = "Report comment thành công"
            };
        }
    }
}
