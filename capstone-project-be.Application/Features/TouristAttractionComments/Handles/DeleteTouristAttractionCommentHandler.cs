using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class DeleteTouristAttractionCommentHandler : IRequestHandler<DeleteTouristAttractionCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;


        public DeleteTouristAttractionCommentHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(DeleteTouristAttractionCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionCommentId, out int touristAttractionCommentId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttractionCommentList = await _unitOfWork.TouristAttractionCommentRepository.
                Find(acc => acc.TouristAttractionCommentId == touristAttractionCommentId);

            if (touristAttractionCommentList.Count() == 0) return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy comment với Id: {touristAttractionCommentId}"
            };

            var touristAttractionComment = touristAttractionCommentList.First();

            var touristAttractionCommentPhotos = await _unitOfWork.TouristAttractionCommentPhotoRepository.
                Find(tcp => tcp.TouristAttractionCommentId == touristAttractionCommentId);
            foreach (var item in touristAttractionCommentPhotos)
            {
                await _storageRepository.DeleteFileAsync(item.SavedFileName);
            }
            await _unitOfWork.TouristAttractionCommentPhotoRepository.DeleteRange(touristAttractionCommentPhotos);

            await _unitOfWork.TouristAttractionCommentRepository.Delete(touristAttractionComment);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Message = "Xóa comment thành công"
            };
        }
    }
}
