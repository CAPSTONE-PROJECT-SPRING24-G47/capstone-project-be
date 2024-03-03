using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Handles
{
    public class GetAcc_AccCategoriesHandler : IRequestHandler<GetAcc_AccCategoriesRequest, IEnumerable<Acc_AccCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAcc_AccCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Acc_AccCategoryDTO>> Handle(GetAcc_AccCategoriesRequest request, CancellationToken cancellationToken)
        {
            var acc_accCategoryList = await _unitOfWork.Acc_AccCategoryRepository.GetAll();

            return _mapper.Map<IEnumerable<Acc_AccCategoryDTO>>(acc_accCategoryList);
        }
    }
}
