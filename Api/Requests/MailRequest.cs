namespace Api.Requests
{
    public class MailRequest
    {
        // @TODO: change to List
        public string To { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
