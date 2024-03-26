using AutoMapper;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, IEnumerable<UserDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetUsersHandler(IUnitOfWork unitOfWork, IStorageRepository blogPhotoStorageRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _storageRepository = blogPhotoStorageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var userList = await _unitOfWork.UserRepository.GetAll();
            foreach (var user in userList)
            {
                if (!string.IsNullOrWhiteSpace(user.SavedFileName))
                {
                    user.SignedUrl = await _storageRepository.GetSignedUrlAsync(user.SavedFileName);
                }

            }
            var userListMapped = _mapper.Map<IEnumerable<UserDTO>>(userList);
            return userListMapped;
        }
    }
}
