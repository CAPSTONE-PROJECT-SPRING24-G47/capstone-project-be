using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionCommentPhotos;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetCommentsByTouristAttractionIdHandler : IRequestHandler<GetCommentsByTouristAttractionIdRequest, BaseResponse<TouristAttractionCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetCommentsByTouristAttractionIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<BaseResponse<TouristAttractionCommentDTO>> Handle(GetCommentsByTouristAttractionIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionId, out int TouristAttractionId))
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var comments = await _unitOfWork.TouristAttractionCommentRepository.
                Find(rc => rc.TouristAttractionId == TouristAttractionId);

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            comments = comments.OrderByDescending(c => c.CreatedAt).Skip(skip).Take(pageSize);
            var touristAttractionComments = _mapper.Map<IEnumerable<TouristAttractionCommentDTO>>(comments);

            foreach (var rc in touristAttractionComments)
            {
                var touristAttractionCommentPhotoList = _mapper.Map<IEnumerable<TouristAttractionCommentPhotoDTO>>
                (await _unitOfWork.TouristAttractionCommentPhotoRepository.
                Find(tcp => tcp.TouristAttractionCommentId == rc.TouristAttractionCommentId));

                foreach (var item in touristAttractionCommentPhotoList)
                {
                    item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
                }
                rc.TouristAttractionCommentPhotos = touristAttractionCommentPhotoList;
            }

            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<TouristAttractionCommentDTO>>(touristAttractionComments)
            };
        }
    }
}
