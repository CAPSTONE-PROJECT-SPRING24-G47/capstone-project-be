using AutoMapper;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Features.BlogPhotos.Requests;
using capstone_project_be.Application.Interfaces;

namespace capstone_project_be.Application.Features.BlogPhotos.Handles
{
    public class GetBlogPhotosHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlogPhotoStorageRepository _blogPhotoStorageRepository;
        private readonly IMapper _mapper;

        public GetBlogPhotosHandler(IUnitOfWork unitOfWork, IBlogPhotoStorageRepository blogPhotoStorageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _blogPhotoStorageRepository = blogPhotoStorageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogPhotoDTO>> Handle(GetBlogPhotosRequest request, CancellationToken cancellationToken)
        {
            var blogPhotos = await _unitOfWork.BlogPhotoRepository.GetAll();
            foreach (var blogPhoto in blogPhotos)
            {
                if (!string.IsNullOrWhiteSpace(blogPhoto.SavedFileName))
                {
                    blogPhoto.SignedUrl = await _blogPhotoStorageRepository.GetSignedUrlAsync(blogPhoto.SavedFileName);
                }

            }
            return _mapper.Map<IEnumerable<BlogPhotoDTO>>(blogPhotos);
        }
    }
}
