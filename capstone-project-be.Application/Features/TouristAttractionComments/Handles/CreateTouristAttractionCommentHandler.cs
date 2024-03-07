﻿using AutoMapper;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Handles
{
    public class CreateTouristAttractionCommentHandler : IRequestHandler<CreateTouristAttractionCommentRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTouristAttractionCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateTouristAttractionCommentRequest request, CancellationToken cancellationToken)
        {
            var TouristAttractionCommentData = request.TouristAttractionCommentData;
            var TouristAttractionComment = _mapper.Map<TouristAttractionComment>(TouristAttractionCommentData);
            TouristAttractionComment.IsReported = false;
            TouristAttractionComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == TouristAttractionComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {TouristAttractionComment.UserId}"
                };
            }

            var TouristAttractionList = await _unitOfWork.TouristAttractionRepository.Find(a => a.TouristAttractionId == TouristAttractionComment.TouristAttractionId);
            if (!TouristAttractionList.Any())
            {
                return new BaseResponse<TouristAttractionCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại TouristAttraction với Id : {TouristAttractionComment.TouristAttractionId}"
                };
            }

            await _unitOfWork.TouristAttractionCommentRepository.Add(TouristAttractionComment);
            await _unitOfWork.Save();

            return new BaseResponse<TouristAttractionCommentDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }
    }
}