using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Handles
{
    public class DeleteAccommodationCommentHandler : IRequestHandler<DeleteAccommodationCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;

        public DeleteAccommodationCommentHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(DeleteAccommodationCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.AccommodationCommentId, out int accommodationCommentId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodationCommentList = await _unitOfWork.AccommodationCommentRepository.
                Find(acc => acc.AccommodationCommentId == accommodationCommentId);

            if (accommodationCommentList.Count() == 0) return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy comment với Id: {accommodationCommentId}"
            };

            var accommodationComment = accommodationCommentList.First();

            var accommodationCommentPhotos = await _unitOfWork.AccommodationCommentPhotoRepository.
                Find(acp => acp.AccommodationCommentId == accommodationCommentId);
            foreach (var item in accommodationCommentPhotos)
            {
                await _storageRepository.DeleteFileAsync(item.SavedFileName);
            }
            await _unitOfWork.AccommodationCommentPhotoRepository.DeleteRange(accommodationCommentPhotos);

            await _unitOfWork.AccommodationCommentRepository.Delete(accommodationComment);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationCommentDTO>()
            {
                IsSuccess = true,
                Message = "Xóa comment thành công"
            };
        }
    }
}
