using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetTouristAttractionCommentsHandler : IRequestHandler<GetTouristAttractionCommentsRequest, IEnumerable<TouristAttractionCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTouristAttractionCommentsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TouristAttractionCommentDTO>> Handle(GetTouristAttractionCommentsRequest request, CancellationToken cancellationToken)
        {
            var TouristAttractionComments = await _unitOfWork.TouristAttractionCommentRepository.GetAll();

            return _mapper.Map<IEnumerable<TouristAttractionCommentDTO>>(TouristAttractionComments);
        }
    }
}
