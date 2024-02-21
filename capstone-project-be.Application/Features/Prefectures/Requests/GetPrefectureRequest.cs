using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Requests
{
    public class GetPrefectureRequest(string prefectureId) : IRequest<BaseResponse<PrefectureDTO>>
    {
        public string PrefectureId { get; set; } = prefectureId;

    }
}
