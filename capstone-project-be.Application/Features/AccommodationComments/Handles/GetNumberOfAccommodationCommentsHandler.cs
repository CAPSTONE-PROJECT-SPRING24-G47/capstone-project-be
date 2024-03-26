using AutoMapper;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class GetNumberOfAccommodationCommentsHandler : IRequestHandler<GetNumberOfAccommodationCommentsRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfAccommodationCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfAccommodationCommentsRequest request, CancellationToken cancellationToken)
        {
            var accommodationComment = await _unitOfWork.AccommodationCommentRepository.GetAll();

            var result = accommodationComment.ToList().Count();

            return result;
        }
    }
}
