using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Handles
{
    public class GetRes_ResCategoriesByResIdHandler : IRequestHandler<GetRes_ResCategoriesByResIdRequest, IEnumerable<Res_ResCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRes_ResCategoriesByResIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Res_ResCategoryDTO>> Handle(GetRes_ResCategoriesByResIdRequest request, CancellationToken cancellationToken)
        {
            var restaurantId = int.Parse(request.RestaurantId);
            var res_resCategoryList = await _unitOfWork.Res_ResCategoryRepository.Find(r => r.RestaurantId == restaurantId);

            return _mapper.Map<IEnumerable<Res_ResCategoryDTO>>(res_resCategoryList);
        }
    }
}
