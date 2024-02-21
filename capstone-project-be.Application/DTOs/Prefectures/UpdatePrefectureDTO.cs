namespace capstone_project_be.Application.DTOs.Prefectures
{
    public class UpdatePrefectureDTO
    {
        public required int RegionId { get; set; }
        public required string PrefectureName { get; set; }
        public required string PrefectureDescription { get; set; }
    }
}
