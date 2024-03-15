using AutoMapper;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, BaseResponse<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
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

            return new BaseResponse<UserDTO>()
            {
                IsSuccess = true,
                Data = new List<UserDTO> { _mapper.Map<UserDTO>(user) }
            };
        }
    }
}
