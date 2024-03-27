using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationCommentPhotos;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class CreateAccommodationCommentHandler : IRequestHandler<CreateAccommodationCommentRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateAccommodationCommentHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateAccommodationCommentRequest request, CancellationToken cancellationToken)
        {
            var accommodationCommentData = request.AccommodationCommentData;
            var accommodationComment = _mapper.Map<AccommodationComment>(accommodationCommentData);
            accommodationComment.IsReported = false;
            accommodationComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == accommodationComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {accommodationComment.UserId}"
                };
            }

            var accommodationList = await _unitOfWork.AccommodationRepository.Find(a => a.AccommodationId == accommodationComment.AccommodationId);
            if (!accommodationList.Any())
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại nơi ở với Id : {accommodationComment.AccommodationId}"
                };
            }
            await _unitOfWork.AccommodationCommentRepository.Add(accommodationComment);
            await _unitOfWork.Save();

            accommodationComment = (await _unitOfWork.AccommodationCommentRepository.
                Find(ac => ac.UserId == accommodationComment.UserId)).OrderByDescending(ac => ac.CreatedAt).First();
            var accommodationCommentId = accommodationComment.AccommodationCommentId;
            var photoData = accommodationCommentData.Photos;
            if (photoData != null)
            {
                var accommodationCommentPhotos = new List<CRUDAccommodationCommentPhotoDTO>();
                foreach (var photo in photoData)
                {
                    if (photo != null)
                    {
                        var savedFileName = GenerateFileNameToSave(photo.FileName);
                        accommodationCommentPhotos.Add(
                            new CRUDAccommodationCommentPhotoDTO
                            {
                                AccommodationCommentId = accommodationCommentId,
                                PhotoURL = await _storageRepository.UpLoadFileAsync(photo, savedFileName),
                                SavedFileName = savedFileName
                            }
                            );
                    }
                }
                var accommodationCommentPhotoList = _mapper.Map<IEnumerable<AccommodationCommentPhoto>>(accommodationCommentPhotos);
                await _unitOfWork.AccommodationCommentPhotoRepository.AddRange(accommodationCommentPhotoList);
                await _unitOfWork.Save();
            }

            return new BaseResponse<AccommodationCommentDTO>()
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
