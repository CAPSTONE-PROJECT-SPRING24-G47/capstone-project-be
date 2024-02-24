using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

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
                accommodation.status = "Approved";
            }
            else accommodation.status = "Processing";

            await _unitOfWork.AccommodationRepository.Add(accommodation);
            await _unitOfWork.Save();

            return new BaseResponse<AccommodationDTO>()
            {
                IsSuccess = true,
                Message = "Thêm nơi ở mới thành công"
            };
        }
    }
}
