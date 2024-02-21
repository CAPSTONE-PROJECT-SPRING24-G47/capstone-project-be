using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Requests
{
    public class DeletePrefectureRequest(string prefectureId) : IRequest<object>
    {
        public string PrefectureId { get; set; } = prefectureId;
    }
}
