using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Handles
{
    public class CreateAcc_AccCategoryHandler : IRequestHandler<CreateAcc_AccCategoryRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAcc_AccCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateAcc_AccCategoryRequest request, CancellationToken cancellationToken)
        {
            var acc_AccCategoryData = request.Acc_AccCategoryData;
            var acc_accCatagory = _mapper.Map<Accommodation_AccommodationCategory>(acc_AccCategoryData);
            var accommodationList = await _unitOfWork.AccommodationRepository.
                Find(ac => ac.AccommodationId == acc_AccCategoryData.AccommodationId);
            if (!accommodationList.Any())
            {
                return new BaseResponse<CRUDAcc_AccCategoryDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại nơi ở với ID = {acc_accCatagory.AccommodationId}"
                };
            }
            var accommodationCategoryList = await _unitOfWork.AccommodationCategoryRepository.
                Find(ac => ac.AccommodationCategoryId == acc_AccCategoryData.AccommodationCategoryId);
            if (!accommodationCategoryList.Any())
            {
                return new BaseResponse<CRUDAcc_AccCategoryDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại AccommodationCategory với CategoryID = {acc_accCatagory.AccommodationCategoryId}"
                };
            }

            await _unitOfWork.Acc_AccCategoryRepository.Add(acc_accCatagory);
            await _unitOfWork.Save();

            return new BaseResponse<CRUDAcc_AccCategoryDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }
    }
}
