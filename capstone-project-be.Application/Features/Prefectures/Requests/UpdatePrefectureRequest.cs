using capstone_project_be.Application.DTOs.Prefectures;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Requests
{
    public class UpdatePrefectureRequest(string prefectureId, UpdatePrefectureDTO prefectureData) : IRequest<object>
    {
        public UpdatePrefectureDTO PrefectureData { get; set; } = prefectureData;
        public string PrefectureId { get; set; } = prefectureId;
    }
}
