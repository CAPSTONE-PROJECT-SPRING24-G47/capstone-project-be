using AutoMapper;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.Features.Prefectures.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Handles
{
    public class GetPrefectureHandler : IRequestHandler<GetPrefectureRequest, BaseResponse<PrefectureDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPrefectureHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<PrefectureDTO>> Handle(GetPrefectureRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.PrefectureId, out int prefectureId))
            {
                return new BaseResponse<PrefectureDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var prefecture = await _unitOfWork.PrefectureRepository.GetByID(prefectureId);

            return new BaseResponse<PrefectureDTO>()
            {
                IsSuccess = true,
                Data = new List<PrefectureDTO> { _mapper.Map<PrefectureDTO>(prefecture) }
            };
        }
    }
}
