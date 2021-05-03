namespace TransactionalEmail.Core.Options
{
    public class MailSettingsOptions
    {
        public RetryPolicyOptions RetryPolicy { get; set; } = new RetryPolicyOptions();

        public FromOptions From { get; set; } = new FromOptions();
    }
}
