using AutoMapper;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.Features.Prefectures.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Handles
{
    public class UpdatePrefectureHandler : IRequestHandler<UpdatePrefectureRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePrefectureHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(UpdatePrefectureRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.PrefectureId, out int prefectureId))
            {
                return new BaseResponse<PrefectureDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var prefecture = _mapper.Map<Prefecture>(request.PrefectureData);
            prefecture.PrefectureId = prefectureId;

            await _unitOfWork.PrefectureRepository.Update(prefecture);
            await _unitOfWork.Save();

            return new BaseResponse<PrefectureDTO>()
            {
                IsSuccess = true,
                Message = "Update tỉnh thành công"
            };
        }
    }
}
