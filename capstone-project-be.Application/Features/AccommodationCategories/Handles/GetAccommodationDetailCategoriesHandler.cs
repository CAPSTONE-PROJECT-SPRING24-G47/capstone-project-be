using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.AccommodationCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationCategories.Handles
{
    public class GetAccommodationDetailCategoriesHandler : IRequestHandler<GetAccommodationDetailCategoriesRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetAccommodationDetailCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(GetAccommodationDetailCategoriesRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int accommodationId))
            {
                return new BaseResponse<AccommodationCategoriesDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var list = await _unitOfWork.Acc_AccCategoryRepository.GetAccommodationDetailCategories(accommodationId);

            return new BaseResponse<AccommodationCategoriesDTO>()
            {
                IsSuccess = true,
                Data = list
            };
        }
    }
}
