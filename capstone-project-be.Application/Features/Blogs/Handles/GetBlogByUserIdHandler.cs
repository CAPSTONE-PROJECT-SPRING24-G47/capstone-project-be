using AutoMapper;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetBlogByUserIdHandler : IRequestHandler<GetBlogByUserIdRequest, BaseResponse<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BlogDTO>> Handle(GetBlogByUserIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.UserId, out int userId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var blogs = await _unitOfWork.BlogRepository.
                Find(b => b.UserId == userId);
            if (!blogs.Any())
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Không có blog phù hợp",
                    IsSuccess = false
                };
            }

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<BlogDTO>>(blogs)
            };
        }
    }
}
