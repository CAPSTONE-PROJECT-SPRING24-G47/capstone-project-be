using AutoMapper;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Features.BlogPhotos.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.BlogPhotos.Handles
{
    public class GetBlogPhotosHandler : IRequestHandler<GetBlogPhotosRequest, IEnumerable<BlogPhotoDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetBlogPhotosHandler(IUnitOfWork unitOfWork, IStorageRepository blogPhotoStorageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = blogPhotoStorageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogPhotoDTO>> Handle(GetBlogPhotosRequest request, CancellationToken cancellationToken)
        {
            var blogPhotos = await _unitOfWork.BlogPhotoRepository.GetAll();
            foreach (var blogPhoto in blogPhotos)
            {
                if (!string.IsNullOrWhiteSpace(blogPhoto.SavedFileName))
                {
                    blogPhoto.SignedUrl = await _storageRepository.GetSignedUrlAsync(blogPhoto.SavedFileName);
                }

            }
            return _mapper.Map<IEnumerable<BlogPhotoDTO>>(blogPhotos);
        }
    }
}
