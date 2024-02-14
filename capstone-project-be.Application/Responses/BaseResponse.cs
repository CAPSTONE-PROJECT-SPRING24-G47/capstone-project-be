namespace capstone_project_be.Application.Responses
{
    public class BaseResponse<T> where T : class
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
