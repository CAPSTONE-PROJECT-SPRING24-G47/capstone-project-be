using AutoMapper;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Handles
{
    public class GetAccommodationNumberHandler : IRequestHandler<GetAccommodationNumberRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAccommodationNumberHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetAccommodationNumberRequest request, CancellationToken cancellationToken)
        {
            var accommodation = await _unitOfWork.AccommodationRepository.GetAll();

            var result = accommodation.ToList().Count();

            return result;
        }
    }
}
