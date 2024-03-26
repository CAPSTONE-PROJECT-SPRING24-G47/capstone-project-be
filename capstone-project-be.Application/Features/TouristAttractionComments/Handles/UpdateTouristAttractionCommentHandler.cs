using AutoMapper;
using capstone_project_be.Application.DTOs.RestaurantCommentPhotos;
using capstone_project_be.Application.DTOs.TouristAttractionCommentPhotos;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class UpdateTouristAttractionCommentHandler : IRequestHandler<UpdateTouristAttractionCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateTouristAttractionCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(UpdateTouristAttractionCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionCommentId, out int touristAttractionCommentId))
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttractionCommentData = request.UpdateTouristAttractionCommentData;
            var touristAttractionComment = _mapper.Map<TouristAttractionComment>(touristAttractionCommentData);
            touristAttractionComment.TouristAttractionCommentId = touristAttractionCommentId;
            touristAttractionComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == touristAttractionComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {touristAttractionComment.UserId}"
                };
            }

            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.Find(a => a.TouristAttractionId == touristAttractionComment.TouristAttractionId);
            if (!touristAttractionList.Any())
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại địa điểm giải trí với Id : {touristAttractionComment.TouristAttractionId}"
                };
            }

            var deletePhotos = touristAttractionCommentData.DeletePhotos;
            if (deletePhotos != null)
            {
                var deletePhotoIds = new List<int>();
                var parts = deletePhotos.Split(",");
                foreach (string part in parts)
                {
                    deletePhotoIds.Add(int.Parse(part));
                }
                var touristAttractionCommentPhotoList = await _unitOfWork.TouristAttractionCommentPhotoRepository.
                    Find(tacp => tacp.TouristAttractionCommentId == touristAttractionCommentId && deletePhotoIds.Contains(tacp.Id));

                foreach (var tacp in touristAttractionCommentPhotoList)
                {
                    await _storageRepository.DeleteFileAsync(tacp.SavedFileName);
                }
                await _unitOfWork.TouristAttractionCommentPhotoRepository.DeleteRange(touristAttractionCommentPhotoList);
            }

            var photoData = touristAttractionCommentData.Photos;
            if (photoData != null)
            {
                var touristAttractionCommentPhotos = new List<CRUDTouristAttractionCommentPhotoDTO>();
                foreach (var photo in photoData)
                {
                    if (photo != null)
                    {
                        var savedFileName = GenerateFileNameToSave(photo.FileName);
                        touristAttractionCommentPhotos.Add(
                            new CRUDTouristAttractionCommentPhotoDTO
                            {
                                TouristAttractionCommentId = touristAttractionCommentId,
                                PhotoURL = await _storageRepository.UpLoadFileAsync(photo, savedFileName),
                                SavedFileName = savedFileName
                            }
                            );
                    }
                }
                var touristAttractionCommentPhotoList = _mapper.Map<IEnumerable<TouristAttractionCommentPhoto>>(touristAttractionCommentPhotos);
                await _unitOfWork.TouristAttractionCommentPhotoRepository.AddRange(touristAttractionCommentPhotoList);
            }

            await _unitOfWork.TouristAttractionCommentRepository.Update(touristAttractionComment);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionCommentDTO>()
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
