using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Handles
{
    public class GetAcc_AccCategoriesByAccIdHandler : IRequestHandler<GetAcc_AccCategoriesByAccIdRequest, IEnumerable<Acc_AccCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAcc_AccCategoriesByAccIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Acc_AccCategoryDTO>> Handle(GetAcc_AccCategoriesByAccIdRequest request, CancellationToken cancellationToken)
        {
            var accommodationId = int.Parse(request.AccommodationId);
            var acc_accCategoryList = await _unitOfWork.Acc_AccCategoryRepository.Find(a => a.AccommodationId == accommodationId);

            return _mapper.Map<IEnumerable<Acc_AccCategoryDTO>>(acc_accCategoryList);
        }
    }
}
