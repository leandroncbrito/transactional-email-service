namespace TransactionalEmail.Core.Options
{
    public class RetryPolicyOptions
    {
        public int Attempts { get; set; } = 1;

        public int SecondsInterval { get; set; } = 0;
    }
}
