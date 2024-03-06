using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class ReportRestaurantCommentHandler : IRequestHandler<ReportRestaurantCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportRestaurantCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(ReportRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantCommentId, out int RestaurantCommentId))
            {
                return new BaseResponse<RestaurantDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var RestaurantCommentList = await _unitOfWork.RestaurantCommentRepository.
                Find(acc => acc.RestaurantCommentId == RestaurantCommentId);

            if (RestaurantCommentList.Count() == 0) return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy comment với Id: {RestaurantCommentId}"
            };

            var RestaurantComment = RestaurantCommentList.First();
            RestaurantComment.IsReported = true;

            await _unitOfWork.RestaurantCommentRepository.Update(RestaurantComment);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Message = "Report comment thành công"
            };
        }
    }
}
