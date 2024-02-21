using AutoMapper;
using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.Features.Prefectures.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Handles
{
    public class CreatePrefectureHandler : IRequestHandler<CreatePrefectureRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePrefectureHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(CreatePrefectureRequest request, CancellationToken cancellationToken)
        {
            var prefectureData = request.PrefectureData;
            var prefecture = _mapper.Map<Prefecture>(prefectureData);

            await _unitOfWork.PrefectureRepository.Add(prefecture);
            await _unitOfWork.Save();

            return new BaseResponse<PrefectureDTO>()
            {
                IsSuccess = true,
                Message = "Thêm thành công tỉnh mới"
            };
        }
    }
}
