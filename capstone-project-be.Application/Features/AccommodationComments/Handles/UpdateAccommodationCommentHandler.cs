using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class UpdateAccommodationCommentHandler : IRequestHandler<UpdateAccommodationCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAccommodationCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateAccommodationCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationCommentId, out int accommodationCommentId))
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodationComment = _mapper.Map<AccommodationComment>(request.UpdateAccommodationCommentData);
            accommodationComment.AccommodationCommentId = accommodationCommentId;
            accommodationComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == accommodationComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {accommodationComment.UserId}"
                };
            }

            var accommodationList = await _unitOfWork.AccommodationRepository.Find(a => a.AccommodationId == accommodationComment.AccommodationId);
            if (!accommodationList.Any())
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại nơi ở với Id : {accommodationComment.AccommodationId}"
                };
            }

            await _unitOfWork.AccommodationCommentRepository.Update(accommodationComment);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Message = "Update comment thành công"
            };
        }
    }
}
