using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class CreateAccommodationCommentHandler : IRequestHandler<CreateAccommodationCommentRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAccommodationCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateAccommodationCommentRequest request, CancellationToken cancellationToken)
        {
            var accommodationCommentData = request.AccommodationCommentData;
            var accommodationComment = _mapper.Map<AccommodationComment>(accommodationCommentData);
            accommodationComment.IsReported = false;
            accommodationComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == accommodationComment.UserId);
            if(!userList.Any()) 
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

            await _unitOfWork.AccommodationCommentRepository.Add(accommodationComment);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }
    }
}
