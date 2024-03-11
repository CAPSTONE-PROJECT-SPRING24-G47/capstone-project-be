namespace capstone_project_be.Domain.Entities
{
    public class Region
    {
        public required int RegionId { get; set; }
        public required string RegionName { get; set; }
        public required string RegionDescription { get; set;}

        public IEnumerable<Prefecture> Prefectures { get; set; }
    }
}
