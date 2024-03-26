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
            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            accommodationComments = accommodationComments.Skip(skip).Take(pageSize).OrderByDescending(ac => ac.CreatedAt);

            return _mapper.Map<IEnumerable<AccommodationCommentDTO>>(accommodationComments);
        }
    }
}
