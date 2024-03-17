using AutoMapper;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Features.BlogPhotos.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.BlogPhotos.Handles
{
    public class CreateBlogPhotoHandler : IRequestHandler<CreateBlogPhotoRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateBlogPhotoHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateBlogPhotoRequest request, CancellationToken cancellationToken)
        {
            var blogPhotoData = request.BlogPhotoData;
            var blogPhoto = _mapper.Map<BlogPhoto>(blogPhotoData);
            if (blogPhoto.Photo != null)
            {
                blogPhoto.SavedFileName = GenerateFileNameToSave(blogPhoto.Photo.FileName);
                blogPhoto.PhotoURL = await _storageRepository.UpLoadFileAsync(blogPhoto.Photo, blogPhoto.SavedFileName);
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
            return $"{fileName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
