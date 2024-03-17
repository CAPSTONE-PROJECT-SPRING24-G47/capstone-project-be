using AutoMapper;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Features.BlogPhotos.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;

namespace capstone_project_be.Application.Features.BlogPhotos.Handles
{
    public class GetBlogPhotoHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlogPhotoStorageRepository _blogPhotoStorageRepository;
        private readonly IMapper _mapper;

        public GetBlogPhotoHandler(IUnitOfWork unitOfWork, IBlogPhotoStorageRepository blogPhotoStorageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _blogPhotoStorageRepository = blogPhotoStorageRepository;
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
                blogPhoto.SignedUrl = await _blogPhotoStorageRepository.GetSignedUrlAsync(blogPhoto.SavedFileName);
            }

            return new BaseResponse<BlogPhotoDTO>()
            {
                IsSuccess = true,
                Data = new List<BlogPhotoDTO> { _mapper.Map<BlogPhotoDTO>(blogPhoto) }
            };
        }
    }
}
