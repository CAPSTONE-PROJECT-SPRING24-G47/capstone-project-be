using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class GetCommentByUserIdAndAccIdHandler : IRequestHandler<GetCommentByUserIdAndAccIdRequest, BaseResponse<AccommodationCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentByUserIdAndAccIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AccommodationCommentDTO>> Handle(GetCommentByUserIdAndAccIdRequest request, CancellationToken cancellationToken)
        {
            var accommodationId = int.Parse(request.AccommodationId);
            var userId = int.Parse(request.UserId);

            var comments = await _unitOfWork.AccommodationCommentRepository.
                Find(ac => ac.AccommodationId == accommodationId && ac.UserId == userId);
            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<AccommodationCommentDTO>>(comments)
            };
        }
    }

}
