using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class UserListHandler : IRequestHandler<UserListRequest, IEnumerable<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserListHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Handle(UserListRequest request, CancellationToken cancellationToken)
        {
            var userList = await _unitOfWork.UserRepository.GetAll();
            var userListMapped = _mapper.Map<IEnumerable<UserDTO>>(userList);
            return userListMapped;
        }
    }
}
