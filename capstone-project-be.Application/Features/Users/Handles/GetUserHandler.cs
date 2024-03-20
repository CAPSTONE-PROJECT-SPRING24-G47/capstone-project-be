using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, BaseResponse<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetUserHandler(IUnitOfWork unitOfWork, IStorageRepository storageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<UserDTO>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.UserId, out int userId))
            {
                return new BaseResponse<UserDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var user = await _unitOfWork.UserRepository.GetByID(userId);
            if (!string.IsNullOrWhiteSpace(user.SavedFileName))
            {
                user.SignedUrl = await _storageRepository.GetSignedUrlAsync(user.SavedFileName);
            }

            return new BaseResponse<UserDTO>()
            {
                IsSuccess = true,
                Data = new List<UserDTO> { _mapper.Map<UserDTO>(user) }
            };
        }
    }
}
