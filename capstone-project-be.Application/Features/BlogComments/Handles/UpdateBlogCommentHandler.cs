using AutoMapper;
using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class UpdateBlogCommentHandler : IRequestHandler<UpdateBlogCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBlogCommentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdateBlogCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogCommentId, out int BlogCommentId))
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var BlogComment = _mapper.Map<BlogComment>(request.UpdateBlogCommentData);
            BlogComment.BlogCommentId = BlogCommentId;
            BlogComment.CreatedAt = DateTime.Now;

            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == BlogComment.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại user với Id : {BlogComment.UserId}"
                };
            }

            var BlogList = await _unitOfWork.BlogRepository.Find(a => a.BlogId == BlogComment.BlogId);
            if (!BlogList.Any())
            {
                return new BaseResponse<BlogCommentDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại blog với Id : {BlogComment.BlogId}"
                };
            }

            await _unitOfWork.BlogCommentRepository.Update(BlogComment);
            await _unitOfWork.Save();

            return new BaseResponse<BlogCommentDTO>()
            {
                IsSuccess = true,
                Message = "Update comment thành công"
            };
        }
    }
}
