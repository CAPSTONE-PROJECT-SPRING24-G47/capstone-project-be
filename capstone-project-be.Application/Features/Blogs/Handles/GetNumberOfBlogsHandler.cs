using AutoMapper;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetNumberOfBlogsHandler : IRequestHandler<GetNumberOfBlogsRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetNumberOfBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNumberOfBlogsRequest request, CancellationToken cancellationToken)
        {
            var blogs = await _unitOfWork.BlogRepository.GetAll();

            var result = blogs.ToList().Count();

            return result;
        }
    }
}
