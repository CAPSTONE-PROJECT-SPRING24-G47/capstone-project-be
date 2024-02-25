using capstone_project_be.Application.DTOs.Search;
using MediatR;

namespace capstone_project_be.Application.Features.Search.Requests
{
    public class SearchRequest(SearchDTO searchData) : IRequest<object>
    {
        public SearchDTO SearchData { get; set; } = searchData;
    }
}
