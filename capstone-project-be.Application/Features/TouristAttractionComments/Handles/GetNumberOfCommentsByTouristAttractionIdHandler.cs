using AutoMapper;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetNumberOfCommentsByTouristAttractionIdHandler : IRequestHandler<GetNumberOfCommentsByTouristAttractionIdRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfCommentsByTouristAttractionIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfCommentsByTouristAttractionIdRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionId = int.Parse(request.TouristAttractionId);

            var comments = await _unitOfWork.TouristAttractionCommentRepository.
                Find(tc => tc.TouristAttractionId == touristAttractionId);

            var result = comments.ToList().Count();

            return result;
        }
    }
}
