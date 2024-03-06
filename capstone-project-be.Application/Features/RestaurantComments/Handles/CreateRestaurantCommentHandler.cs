using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Handles
{
    public class CreateRestaurantCommentHandler : IRequestHandler<CreateRestaurantCommentRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRestaurantCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateRestaurantCommentRequest request, CancellationToken cancellationToken)
        {
            var RestaurantCommentData = request.RestaurantCommentData;
            var RestaurantComment = _mapper.Map<RestaurantComment>(RestaurantCommentData);
            RestaurantComment.IsReported = false;
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

            await _unitOfWork.RestaurantCommentRepository.Add(RestaurantComment);
            await _unitOfWork.Save();

            return new BaseResponse<RestaurantCommentDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }
    }
}
