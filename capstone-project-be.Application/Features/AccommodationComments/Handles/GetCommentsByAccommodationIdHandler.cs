using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class GetCommentsByAccommodationIdHandler : IRequestHandler<GetCommentsByAccommodationIdRequest, BaseResponse<AccommodationCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentsByAccommodationIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccommodationCommentDTO>> Handle(GetCommentsByAccommodationIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationId, out int AccommodationId))
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var comments = await _unitOfWork.AccommodationCommentRepository.
                Find(ac => ac.AccommodationId == AccommodationId);

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            comments = comments.Skip(skip).Take(pageSize).OrderByDescending(c => c.CreatedAt);

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<AccommodationCommentDTO>>(comments)
            };
        }
    }
}
