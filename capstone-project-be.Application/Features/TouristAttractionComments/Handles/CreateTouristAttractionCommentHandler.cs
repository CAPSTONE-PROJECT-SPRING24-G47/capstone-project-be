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
    public class CreateTouristAttractionCommentHandler : IRequestHandler<CreateTouristAttractionCommentRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateTouristAttractionCommentHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(CreateTouristAttractionCommentRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionCommentData = request.TouristAttractionCommentData;
            var touristAttractionComment = _mapper.Map<TouristAttractionComment>(touristAttractionCommentData);
            touristAttractionComment.IsReported = false;
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

            var TouristAttractionList = await _unitOfWork.TouristAttractionRepository.Find(a => a.TouristAttractionId == touristAttractionComment.TouristAttractionId);
            if (!TouristAttractionList.Any())
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại TouristAttraction với Id : {touristAttractionComment.TouristAttractionId}"
                };
            }

            await _unitOfWork.TouristAttractionCommentRepository.Add(touristAttractionComment);
            await _unitOfWork.Save();

            touristAttractionComment = (await _unitOfWork.TouristAttractionCommentRepository.
                Find(tac => tac.UserId == touristAttractionComment.UserId)).OrderByDescending(tc => tc.CreatedAt).First();
            var touristAttractionCommentId = touristAttractionComment.TouristAttractionCommentId;
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
                await _unitOfWork.Save();
            }
            
            return new BaseResponse<TouristAttractionCommentDTO>()
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
