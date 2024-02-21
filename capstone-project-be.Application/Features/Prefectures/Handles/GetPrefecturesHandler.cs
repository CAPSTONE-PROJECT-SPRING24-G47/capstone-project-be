using AutoMapper;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.Features.Prefectures.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Handles
{
    public class GetPrefecturesHandler : IRequestHandler<GetPrefecturesRequest, IEnumerable<PrefectureDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrefecturesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PrefectureDTO>> Handle(GetPrefecturesRequest request, CancellationToken cancellationToken)
        {
            var regions = await _unitOfWork.PrefectureRepository.GetAll();

            return _mapper.Map<IEnumerable<PrefectureDTO>>(regions);
        }
    }
}
