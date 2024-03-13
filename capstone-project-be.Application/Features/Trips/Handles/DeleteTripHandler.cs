using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Handles
{
    public class DeleteTripHandler : IRequestHandler<DeleteTripRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTripHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTripRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.TripId, out int tripId))
            {
                return new BaseResponse<TripDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var tripList = await _unitOfWork.TripRepository.Find(t => t.TripId == tripId);

            if (tripList.Count() == 0) return new BaseResponse<TripDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy chuyến đi với Id: {tripId}"
            };

            var trip = tripList.First();

            await _unitOfWork.TripRepository.Delete(trip);
            await _unitOfWork.Save();

            return new BaseResponse<TripDTO>()
            {
                IsSuccess = true,
                Message = "Xóa chuyến đi thành công"
            };
        }
    }
}
