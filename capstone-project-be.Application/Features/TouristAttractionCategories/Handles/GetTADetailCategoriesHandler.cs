using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.Features.TouristAttractionCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionCategories.Handles
{
    public class GetTADetailCategoriesHandler : IRequestHandler<GetTADetailCategoriesRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTADetailCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(GetTADetailCategoriesRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int taId))
            {
                return new BaseResponse<TouristAttractionCategoriesDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var list = await _unitOfWork.TA_TACategoryRepository.GetTADetailCategories(taId);

            return new BaseResponse<TouristAttractionCategoriesDTO>()
            {
                IsSuccess = true,
                Data = list
            };
        }
    }
}
