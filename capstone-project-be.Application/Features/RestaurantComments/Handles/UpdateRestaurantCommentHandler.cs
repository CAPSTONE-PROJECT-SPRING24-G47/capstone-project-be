using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class UpdateRestaurantCommentHandler : IRequestHandler<UpdateRestaurantCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRestaurantCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.RestaurantCommentId, out int RestaurantCommentId))
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var RestaurantComment = _mapper.Map<RestaurantComment>(request.UpdateRestaurantCommentData);
            RestaurantComment.RestaurantCommentId = RestaurantCommentId;
            RestaurantComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == RestaurantComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {RestaurantComment.UserId}"
                };
            }

            var RestaurantList = await _unitOfWork.RestaurantRepository.Find(a => a.RestaurantId == RestaurantComment.RestaurantId);
            if (!RestaurantList.Any())
            {
                return new BaseResponse<RestaurantCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại Restaurant với Id : {RestaurantComment.RestaurantId}"
                };
            }

            await _unitOfWork.RestaurantCommentRepository.Update(RestaurantComment);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Message = "Update comment thành công"
            };
        }
    }
}
