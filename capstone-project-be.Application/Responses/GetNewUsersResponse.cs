namespace capstone_project_be.Application.Responses
{
    public class GetNewUsersResponse<T> : BaseResponse<T> where T : class
    {
        public double ChangePercentage { get; set; }
    }
}
