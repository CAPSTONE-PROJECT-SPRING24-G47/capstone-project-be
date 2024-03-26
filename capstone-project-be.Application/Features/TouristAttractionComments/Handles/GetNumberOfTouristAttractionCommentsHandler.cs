using AutoMapper;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetNumberOfTouristAttractionCommentsHandler : IRequestHandler<GetNumberOfTouristAttractionCommentsRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfTouristAttractionCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfTouristAttractionCommentsRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionComment = await _unitOfWork.TouristAttractionCommentPhotoRepository.GetAll();

            var result = touristAttractionComment.ToList().Count();

            return result;
        }
    }
}
