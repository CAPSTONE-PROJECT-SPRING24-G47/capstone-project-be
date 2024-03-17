using AutoMapper;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Features.BlogPhotos.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Features.BlogPhotos.Handles
{
    public class CreateBlogPhotoHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlogPhotoStorageRepository _blogPhotoStorageRepository;
        private readonly IMapper _mapper;

        public CreateBlogPhotoHandler(IUnitOfWork unitOfWork, IBlogPhotoStorageRepository blogPhotoStorageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _blogPhotoStorageRepository = blogPhotoStorageRepository;
            _mapper = mapper;
        }

        public async Task<Object> Handle(CreateBlogPhotoRequest request, CancellationToken cancellationToken)
        {
            var blogPhotoData = request.BlogPhotoData;
            var blogPhoto = _mapper.Map<BlogPhoto>(blogPhotoData);
            if (blogPhoto.Photo != null)
            {
                blogPhoto.SavedFileName = GenerateFileNameToSave(blogPhoto.Photo.FileName);
                blogPhoto.SavedUrl = await _blogPhotoStorageRepository.UpLoadFileAsync(blogPhoto.Photo, blogPhoto.SavedFileName);
            }
            await _unitOfWork.BlogPhotoRepository.Add(blogPhoto);
            await _unitOfWork.Save();

            return new BaseResponse<BlogPhotoDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }

        private string? GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
