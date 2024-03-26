using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionCommentPhotos;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class GetTouristAttractionCommentHandler : IRequestHandler<GetTouristAttractionCommentRequest, BaseResponse<TouristAttractionCommentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;

        private readonly IMapper _mapper;

        public GetTouristAttractionCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<BaseResponse<TouristAttractionCommentDTO>> Handle(GetTouristAttractionCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionCommentId, out int touristAttractionCommentId))
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttractionComment = _mapper.Map<TouristAttractionCommentDTO>
                (await _unitOfWork.TouristAttractionCommentRepository.GetByID(touristAttractionCommentId));

            if (touristAttractionComment == null)
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy comment!"
                };
            }

            var touristAttractionCommentPhotoList = _mapper.Map<IEnumerable<TouristAttractionCommentPhotoDTO>>
               (await _unitOfWork.TouristAttractionCommentPhotoRepository.
               Find(tcp => tcp.TouristAttractionCommentId == touristAttractionCommentId));

            foreach (var item in touristAttractionCommentPhotoList)
            {
                item.SignedUrl = await _storageRepository.GetSignedUrlAsync(item.SavedFileName);
            }
            touristAttractionComment.TouristAttractionCommentPhotos = touristAttractionCommentPhotoList;

            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Data = new List<TouristAttractionCommentDTO> { touristAttractionComment }
            };
        }
    }
}
