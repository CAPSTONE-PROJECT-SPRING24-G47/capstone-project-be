using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Handles
{
    public class UpdateAcc_AccCategoryHandler : IRequestHandler<UpdateAcc_AccCategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAcc_AccCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateAcc_AccCategoryRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.Id, out int Id))
            {
                return new BaseResponse<CRUDAcc_AccCategoryDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var acc_accCategory = _mapper.Map<Accommodation_AccommodationCategory>(request.UpdateAcc_AccCategoryData);
            acc_accCategory.Id = Id;

            await _unitOfWork.Acc_AccCategoryRepository.Update(acc_accCategory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDAcc_AccCategoryDTO>()
            {
                IsSuccess = true,
                Message = "Update thành công"
            };
        }
    }
}
