using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
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
            var touristAttractionComments = await _unitOfWork.TouristAttractionCommentRepository.GetAll();

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            touristAttractionComments = touristAttractionComments.OrderByDescending(rc => rc.CreatedAt).Skip(skip).Take(pageSize);

            return _mapper.Map<IEnumerable<TouristAttractionCommentDTO>>(touristAttractionComments);
        }
    }
}
