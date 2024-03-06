using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class GetAccommodationCommentHandler : IRequestHandler<GetAccommodationCommentRequest, BaseResponse<AccommodationCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccommodationCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccommodationCommentDTO>> Handle(GetAccommodationCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationCommentId, out int accommodationCommentId))
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodationComment = await _unitOfWork.AccommodationCommentRepository.GetByID(accommodationCommentId);

            if (accommodationComment == null)
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy comment!"
                };
            }

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Data = new List<AccommodationCommentDTO> { _mapper.Map<AccommodationCommentDTO>(accommodationComment) }
            };
        }
    }
}
