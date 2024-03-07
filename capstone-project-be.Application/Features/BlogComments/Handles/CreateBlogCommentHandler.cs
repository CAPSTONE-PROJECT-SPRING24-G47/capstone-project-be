using AutoMapper;
using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class CreateBlogCommentHandler : IRequestHandler<CreateBlogCommentRequest, object>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBlogCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreateBlogCommentRequest request, CancellationToken cancellationToken)
        {
            var blogCommentData = request.BlogCommentData;
            var blogComment = _mapper.Map<BlogComment>(blogCommentData);
            blogComment.IsReported = false;
            blogComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == blogComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {blogComment.UserId}"
                };
            }

            var blogList = await _unitOfWork.BlogRepository.Find(a => a.BlogId == blogComment.BlogId);
            if (!blogList.Any())
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại blog với Id : {blogComment.BlogId}"
                };
            }

            await _unitOfWork.BlogCommentRepository.Add(blogComment);
            await _unitOfWork.Save();

            return new BaseResponse<BlogCommentDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công"
            };
        }
    }
}
