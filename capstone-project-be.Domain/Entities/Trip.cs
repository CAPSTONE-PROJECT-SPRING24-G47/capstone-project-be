namespace capstone_project_be.Domain.Entities
{
    public class Trip
    {
        public required int TripId { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required float MaxBudget { get; set; }
        public required float MinBudget { get; set; }
        public required bool IsPublic { get; set; } = false;

        //Set quan hệ với các bảng khác ở đây
        public User User { get; set; }
    }
}
