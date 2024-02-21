using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.Features.Prefectures.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Handles
{
    public class DeletePrefectureHandler : IRequestHandler<DeletePrefectureRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePrefectureHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeletePrefectureRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.PrefectureId, out int prefectureId))
            {
                return new BaseResponse<PrefectureDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var prefectureList = await _unitOfWork.PrefectureRepository.Find(prefecture => prefecture.PrefectureId == prefectureId);

            if (prefectureList.Count() == 0) return new BaseResponse<PrefectureDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy tỉnh với id là {prefectureId}"
            };

            var prefecture = prefectureList.First();

            await _unitOfWork.PrefectureRepository.Delete(prefecture);
            await _unitOfWork.Save();

            return new BaseResponse<PrefectureDTO>()
            {
                IsSuccess = true,
                Message = "Xóa tỉnh thành công"
            };
        }
    }
}
