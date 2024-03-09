namespace capstone_project_be.Domain.Entities
{
    public class Prefecture
    {
        public required int PrefectureId { get; set; }
        public required int RegionId { get; set; }
        public required string PrefectureName { get; set; }
        public  required string PrefectureDescription { get; set;}

        public Region Region { get; set;}
        public IEnumerable<City> Cities { get; set; }
    }
}
