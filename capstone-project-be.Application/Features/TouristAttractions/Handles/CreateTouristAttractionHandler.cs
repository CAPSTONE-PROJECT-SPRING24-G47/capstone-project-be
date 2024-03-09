using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Handles
{
    public class CreateTouristAttractionHandler : IRequestHandler<CreateTouristAttractionRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTouristAttractionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateTouristAttractionRequest request, CancellationToken cancellationToken)
        {
            var touristAttractionData = request.TouristAttractionData;
            var touristAttraction = _mapper.Map<TouristAttraction>(touristAttractionData);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == touristAttraction.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<TouristAttractionDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {touristAttraction.UserId}"
                };
            }
            var user = userList.First();
            if (user.RoleId == 3)
            {
                touristAttraction.Status = "Approved";
                touristAttraction.CreatedAt = DateTime.Now;
            }
            else touristAttraction.Status = "Processing";
            touristAttraction.CreatedAt = DateTime.Now;
            await _unitOfWork.TouristAttractionRepository.Add(touristAttraction);
            await _unitOfWork.Save();

            var touristAttractionList = await _unitOfWork.TouristAttractionRepository.
                Find(ta => ta.UserId == touristAttraction.UserId && ta.CreatedAt >= DateTime.Now.AddMinutes(-1));
            if (!touristAttractionList.Any())
                return new BaseResponse<TouristAttractionDTO>()
                {
                    IsSuccess = false,
                    Message = "Thêm mới thất bại"
                };
            var touristAttractionId = touristAttractionList.First().TouristAttractionId;
            var ta_TaCategories = touristAttractionData.TouristAttraction_TouristAttractionCategories;
            var ta_TaCategoryList = _mapper.Map<IEnumerable<TouristAttraction_TouristAttractionCategory>>(ta_TaCategories);
            foreach (var item in ta_TaCategoryList)
            {
                item.TouristAttractionId = touristAttractionId;
            }
            await _unitOfWork.TA_TACategoryRepository.AddRange(ta_TaCategoryList);

            var touristAttractionPhotos = touristAttractionData.TouristAttractionPhotos;
            var touristAttractionPhotoList = _mapper.Map<IEnumerable<TouristAttractionPhoto>>(touristAttractionPhotos);
            foreach (var item in touristAttractionList)
            {
                item.TouristAttractionId = touristAttractionId;
            }
            await _unitOfWork.TouristAttractionPhotoRepository.AddRange(touristAttractionPhotoList);

            return new BaseResponse<TouristAttractionDTO>()
            {
                IsSuccess = true,
                Message = "Thêm địa điểm giải trí mới thành công"
            };
        }
    }
}
