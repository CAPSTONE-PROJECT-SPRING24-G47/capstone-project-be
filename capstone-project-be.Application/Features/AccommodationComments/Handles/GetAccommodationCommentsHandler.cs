using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class GetAccommodationCommentsHandler : IRequestHandler<GetAccommodationCommentsRequest, IEnumerable<AccommodationCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccommodationCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccommodationCommentDTO>> Handle(GetAccommodationCommentsRequest request, CancellationToken cancellationToken)
        {
            var accommodationComments = await _unitOfWork.AccommodationCommentRepository.GetAll();

            return _mapper.Map<IEnumerable<AccommodationCommentDTO>>(accommodationComments);
        }
    }
}
