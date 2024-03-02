using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Handles
{
    public class GetRes_ResCategoriesHandler : IRequestHandler<GetRes_ResCategoriesRequest, IEnumerable<Res_ResCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRes_ResCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Res_ResCategoryDTO>> Handle(GetRes_ResCategoriesRequest request, CancellationToken cancellationToken)
        {
            var res_ResCategoryList = await _unitOfWork.Res_ResCategoryRepository.GetAll();

            return _mapper.Map<IEnumerable<Res_ResCategoryDTO>>(res_ResCategoryList);
        }
    }
}
