namespace TransactionalEmail.Api.Responses
{
    public class ApiResponse
    {
        public ApiResponse() { }

        public ApiResponse(int status, string title, bool success)
        {
            Status = status;
            Title = title;
            Success = success;
        }

        public int Status { get; set; }

        public string Title { get; set; }

        public bool Success { get; set; }
    }
}
