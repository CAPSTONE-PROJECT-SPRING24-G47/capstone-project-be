using AutoMapper;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Handles
{
    public class UpdateTA_TACategoryHandler : IRequestHandler<UpdateTA_TACategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTA_TACategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateTA_TACategoryRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int Id))
            {
                return new BaseResponse<CRUDTA_TACategoryDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var tA_TACategory = _mapper.Map<TouristAttraction_TouristAttractionCategory>(request.UpdateTA_TACategoryData);
            tA_TACategory.Id = Id;

            await _unitOfWork.TA_TACategoryRepository.Update(tA_TACategory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDTA_TACategoryDTO>()
            {
                IsSuccess = true,
                Message = "Update thành công"
            };
        }
    }
}
