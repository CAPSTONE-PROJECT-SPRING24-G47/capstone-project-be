using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class GetNumberOfCommentsByAccommodationIdHandler : IRequestHandler<GetNumberOfCommentsByAccommodationIdRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfCommentsByAccommodationIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfCommentsByAccommodationIdRequest request, CancellationToken cancellationToken)
        {
            var accommodationId = int.Parse(request.AccommodationId);

            var comments = await _unitOfWork.AccommodationCommentRepository.
                Find(ac => ac.AccommodationId == accommodationId);

            var result = comments.ToList().Count();

            return result;
        }
    }
}
