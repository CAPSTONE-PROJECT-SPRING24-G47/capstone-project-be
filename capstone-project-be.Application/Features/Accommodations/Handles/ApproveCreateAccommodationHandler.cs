using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class ApproveCreateAccommodationHandler : IRequestHandler<ApproveCreateAccommodationRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApproveCreateAccommodationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(ApproveCreateAccommodationRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int accommodationId))
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var accommodationList = await _unitOfWork.AccommodationRepository.Find(ac => ac.AccommodationId == accommodationId);

            if (!accommodationList.Any()) return new BaseResponse<AccommodationDTO>()
            {
                Message = "Không tìm thấy nơi ở",
                IsSuccess = false
            };

            var accommodation = accommodationList.First();
            var action = request.Action;
            if (action.Equals("Approve"))
            {
                accommodation.Status = "Approved";
                accommodation.CreatedAt = DateTime.Now;
                await _unitOfWork.AccommodationRepository.Update(accommodation);
                await _unitOfWork.Save();

                return new BaseResponse<AccommodationDTO>()
                {
                    Message = "Yêu cầu được phê duyệt",
                    IsSuccess = true
                };
            }
            else accommodation.Status = "Rejected";
            await _unitOfWork.AccommodationRepository.Update(accommodation);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationDTO>()
            {
                Message = "Yêu cầu bị từ chối",
                IsSuccess = true
            };
        }
    }
}
