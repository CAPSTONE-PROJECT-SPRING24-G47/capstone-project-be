using capstone_project_be.Application.DTOs.Prefectures;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Requests
{
    public class CreatePrefectureRequest(PrefectureDTO prefectureData) : IRequest<object>
    {
        public PrefectureDTO PrefectureData { get; set; } = prefectureData;
    }
}
