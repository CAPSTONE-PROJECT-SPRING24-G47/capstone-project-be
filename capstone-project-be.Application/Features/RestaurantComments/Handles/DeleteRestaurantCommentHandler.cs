using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class DeleteRestaurantCommentHandler : IRequestHandler<DeleteRestaurantCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRestaurantCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteRestaurantCommentRequest request, CancellationToken cancellationToken)
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

            await _unitOfWork.RestaurantCommentRepository.Delete(RestaurantComment);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Message = "Xóa comment thành công"
            };
        }
    }
}
