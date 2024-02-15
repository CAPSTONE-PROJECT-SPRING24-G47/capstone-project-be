namespace capstone_project_be.Application.Responses
{
    public class CodeExpiredResponse<T> : BaseResponse<T> where T : class
    {
        public bool IsExpired { get; set; }
    }
}
