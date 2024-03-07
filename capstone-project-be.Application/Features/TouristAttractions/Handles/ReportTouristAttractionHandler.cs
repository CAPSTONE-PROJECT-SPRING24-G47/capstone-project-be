using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class ReportTouristAttractionHandler : IRequestHandler<ReportTouristAttractionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportTouristAttractionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(ReportTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TouristAttractionId, out int touristAttractionId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.
                Find(acc => acc.TouristAttractionId == touristAttractionId);

            if (touristAttractionList.Count() == 0) return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy địa điểm giải trí với Id: {touristAttractionId}"
            };

            var touristAttraction = touristAttractionList.First();
            touristAttraction.IsReported = true;

            await _unitOfWork.TouristAttractionRepository.Update(touristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Report thành công"
            };
        }
    }
}
