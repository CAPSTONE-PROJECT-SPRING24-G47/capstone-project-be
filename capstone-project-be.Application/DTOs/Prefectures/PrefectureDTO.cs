using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.DTOs.Prefectures
{
    public class PrefectureDTO
    {
        public int PrefectureId { get; set; }
        public int RegionId { get; set; }
        public string PrefectureName { get; set; }
        public string PrefectureDescription { get; set; }
    }
}
