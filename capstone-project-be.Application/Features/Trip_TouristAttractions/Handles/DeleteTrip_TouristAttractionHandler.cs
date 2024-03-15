using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Features.Trip_TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_TouristAttractions.Handles
{
    public class DeleteTrip_TouristAttractionHandler : IRequestHandler<DeleteTrip_TouristAttractionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTrip_TouristAttractionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteTrip_TouristAttractionRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int trip_TouristAttractionId))
            {
                return new BaseResponse<Trip_TouristAttractionDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var trip_TouristAttractionList = await _unitOfWork.Trip_TouristAttractionRepository.
                Find(tta => tta.Id == trip_TouristAttractionId);

            var trip_TouristAttraction = trip_TouristAttractionList.First();

            await _unitOfWork.Trip_TouristAttractionRepository.Delete(trip_TouristAttraction);
            await _unitOfWork.Save();

            return new BaseResponse<Trip_TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Xóa địa điểm giải trí trong chuyến đi thành công"
            };
        }
    }
}
