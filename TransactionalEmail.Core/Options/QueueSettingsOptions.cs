namespace TransactionalEmail.Core.Options
{
    public class QueueSettingsOptions
    {
        public int Capacity { get; set; } = 100;

        public int Attempts { get; set; } = 3;

        public int SecondsInterval { get; set; } = 5;
    }
}
