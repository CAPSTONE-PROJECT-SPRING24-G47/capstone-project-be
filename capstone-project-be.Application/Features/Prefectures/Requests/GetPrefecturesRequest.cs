using capstone_project_be.Application.DTOs.Prefectures;
using MediatR;

namespace capstone_project_be.Application.Features.Prefectures.Requests
{
    public class GetPrefecturesRequest : IRequest<IEnumerable<PrefectureDTO>>
    {
    }
}
