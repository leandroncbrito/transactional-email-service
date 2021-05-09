namespace TransactionalEmail.Api.Responses
{
    public class ApiResponse
    {
        public ApiResponse() { }

        public ApiResponse(int status, string title)
        {
            Status = status;
            Title = title;
        }

        public int Status { get; set; }

        public string Title { get; set; }
    }
}
