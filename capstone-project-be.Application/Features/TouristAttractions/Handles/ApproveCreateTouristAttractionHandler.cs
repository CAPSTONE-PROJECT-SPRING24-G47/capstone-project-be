using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class ApproveCreateTouristAttractionHandler : IRequestHandler<ApproveCreateTouristAttractionRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApproveCreateTouristAttractionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(ApproveCreateTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int touristAttractionId))
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.Find(ta => ta.TouristAttractionId == touristAttractionId);

            if (!touristAttractionList.Any()) return new BaseResponse<TouristAttractionDTO>()
            {
                Message = "Không tìm thấy địa điểm giải trí",
                IsSuccess = false
            };

            var touristAttraction = touristAttractionList.First();
            var action = request.Action;
            if (action.Equals("Approve"))
            {
                touristAttraction.Status = "Approved";
                await _unitOfWork.TouristAttractionRepository.Update(touristAttraction);
                await _unitOfWork.Save();

                return new BaseResponse<TouristAttractionDTO>()
                {
                    Message = "Yêu cầu được phê duyệt",
                    IsSuccess = true
                };
            }
            else touristAttraction.Status = "Rejected";
            await _unitOfWork.TouristAttractionRepository.Update(touristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionDTO>()
            {
                Message = "Yêu cầu bị từ chối",
                IsSuccess = true
            };
        }
    }
}
