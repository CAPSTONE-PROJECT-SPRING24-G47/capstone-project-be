using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;
using System.Xml;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class CreateAccommodationHandler : IRequestHandler<CreateAccommodationRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAccommodationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateAccommodationRequest request, CancellationToken cancellationToken)
        {
            var accommodationData = request.AccommodationData;
            var accommodation = _mapper.Map<Accommodation>(accommodationData);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == accommodation.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {accommodation.UserId}"
                };
            }

            var user = userList.First();
            if (user.RoleId == 3)
            {
                accommodation.Status = "Approved";
            }
            else accommodation.Status = "Processing";
            accommodation.CreatedAt = DateTime.Now;
            await _unitOfWork.AccommodationRepository.Add(accommodation);
            await _unitOfWork.Save();

            var accommodationList = await _unitOfWork.AccommodationRepository.
                Find(a => a.UserId == accommodation.UserId && a.CreatedAt >= DateTime.Now.AddMinutes(-1));
            if(!accommodationList.Any())
                return new BaseResponse<AccommodationDTO>()
                {
                    IsSuccess = false,
                    Message = "Thêm nơi ở mới thất bại"
                };
            var accommodationId = accommodationList.First().AccommodationId;
            var acc_AccCategories = accommodationData.Accommodation_AccommodationCategories;
            var acc_AccCategoryList = _mapper.Map<IEnumerable<Accommodation_AccommodationCategory>>(acc_AccCategories);
            foreach (var item in acc_AccCategoryList)
            {
                item.AccommodationId = accommodationId;
            }
            await _unitOfWork.Acc_AccCategoryRepository.AddRange(acc_AccCategoryList);

            var accommodationPhotos = accommodationData.AccommodationPhotos;
            var accommodationPhotoList = _mapper.Map<IEnumerable<AccommodationPhoto>>(accommodationPhotos);
            foreach (var item in accommodationPhotoList)
            {
                item.AccommodationId = accommodationId;
            }
            await _unitOfWork.AccommodationPhotoRepository.AddRange(accommodationPhotoList);

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nơi ở mới thành công"
            };
        }
    }
}
