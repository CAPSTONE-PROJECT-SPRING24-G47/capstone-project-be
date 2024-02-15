﻿using AutoMapper;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Handles
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileRequest, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProfileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            var data = request.UpdateProfileData;
            var userList = await _unitOfWork.UserRepository.Find(user => user.UserId == data.UserId);
            if (userList.Any())
            {
                var userToUpdate = userList.First();
                userToUpdate.FirstName = data.FirstName;
                userToUpdate.LastName = data.LastName;
                userToUpdate.PictureProfile = data.PictureProfile;
                await _unitOfWork.UserRepository.Update(userToUpdate);
                await _unitOfWork.Save();
                return "Cập nhật thành công !";
            }
            return "Cập nhật thất bại !";
        }
    }
}