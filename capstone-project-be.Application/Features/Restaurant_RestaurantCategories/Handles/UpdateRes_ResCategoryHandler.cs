using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Handles
{
    public class UpdateRes_ResCategoryHandler : IRequestHandler<UpdateRes_ResCategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRes_ResCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateRes_ResCategoryRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int Id))
            {
                return new BaseResponse<CRUDRes_ResCategoryDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var res_resCategory = _mapper.Map<Restaurant_RestaurantCategory>(request.UpdateRes_ResCategoryData);
            res_resCategory.Id = Id;

            await _unitOfWork.Res_ResCategoryRepository.Update(res_resCategory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDRes_ResCategoryDTO>()
            {
                IsSuccess = true,
                Message = "Update thành công"
            };
        }
    }
}
