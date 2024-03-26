using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationCommentPhotos;
using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.AccommodationPhotos;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class UpdateAccommodationCommentHandler : IRequestHandler<UpdateAccommodationCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateAccommodationCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(UpdateAccommodationCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationCommentId, out int accommodationCommentId))
            {
                return new BaseResponse<AccommodationCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodationCommentData = request.UpdateAccommodationCommentData;
            var accommodationComment = _mapper.Map<AccommodationComment>(accommodationCommentData);
            accommodationComment.AccommodationCommentId = accommodationCommentId;
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

            var deletePhotos = accommodationCommentData.DeletePhotos;
            if (deletePhotos != null)
            {
                var deletePhotoIds = new List<int>();
                var parts = deletePhotos.Split(",");
                foreach (string part in parts)
                {
                    deletePhotoIds.Add(int.Parse(part));
                }
                var accommodationCommentPhotoList = await _unitOfWork.AccommodationCommentPhotoRepository.
                    Find(acp => acp.AccommodationCommentId == accommodationCommentId && deletePhotoIds.Contains(acp.Id));

                foreach (var acp in accommodationCommentPhotoList)
                {
                    await _storageRepository.DeleteFileAsync(acp.SavedFileName);
                }
                await _unitOfWork.AccommodationCommentPhotoRepository.DeleteRange(accommodationCommentPhotoList);
            }

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
            }

            await _unitOfWork.AccommodationCommentRepository.Update(accommodationComment);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Message = "Update comment thành công"
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
