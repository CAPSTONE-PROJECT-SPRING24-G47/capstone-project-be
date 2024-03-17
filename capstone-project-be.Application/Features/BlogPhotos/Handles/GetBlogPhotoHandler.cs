using AutoMapper;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Features.BlogPhotos.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogPhotos.Handles
{
    public class GetBlogPhotoHandler : IRequestHandler<GetBlogPhotoRequest, BaseResponse<BlogPhotoDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetBlogPhotoHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BlogPhotoDTO>> Handle(GetBlogPhotoRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogPhotoId, out int blogPhotoId))
            {
                return new BaseResponse<BlogPhotoDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var blogPhotoList = await _unitOfWork.BlogPhotoRepository
                .Find(bp => bp.BlogPhotoId == blogPhotoId);
            var blogPhoto = blogPhotoList.First();
            if (!string.IsNullOrWhiteSpace(blogPhoto.SavedFileName))
            {
                blogPhoto.SignedUrl = await _storageRepository.GetSignedUrlAsync(blogPhoto.SavedFileName);
            }

            return new BaseResponse<BlogPhotoDTO>()
            {
                IsSuccess = true,
                Data = new List<BlogPhotoDTO> { _mapper.Map<BlogPhotoDTO>(blogPhoto) }
            };
        }
    }
}
