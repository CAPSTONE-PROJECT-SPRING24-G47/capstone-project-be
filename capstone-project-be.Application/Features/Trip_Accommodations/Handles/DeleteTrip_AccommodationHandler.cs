using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Handles
{
    public class DeleteTrip_AccommodationHandler : IRequestHandler<DeleteTrip_AccommodationRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTrip_AccommodationHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTrip_AccommodationRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int trip_AccommodationId))
            {
                return new BaseResponse<Trip_AccommodationDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip_AccommodationList = await _unitOfWork.Trip_AccommodationRepository.
                Find(ta => ta.Id == trip_AccommodationId);

            var trip_Accommodation = trip_AccommodationList.First();

            await _unitOfWork.Trip_AccommodationRepository.Delete(trip_Accommodation);
            await _unitOfWork.Save();

            return new BaseResponse<Trip_AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Xóa nơi ở trong chuyến đi thành công"
            };
        }
    }
}
