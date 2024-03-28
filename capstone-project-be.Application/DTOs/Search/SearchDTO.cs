namespace capstone_project_be.Application.DTOs.Search
{
    public class SearchDTO
    {
        public required string Value { get; set; }
        public string Type { get; set; }
        public string Property { get; set; }
        public int Limit { get; set; }

    }
}
