namespace TransactionalEmail.Core.Options
{
    public class MailSettingsOptions
    {
        public int Retries { get; set; }

        public FromOptions From { get; set; } = new FromOptions();
    }
}
