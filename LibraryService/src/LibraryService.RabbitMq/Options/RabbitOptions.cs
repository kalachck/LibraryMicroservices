namespace LibraryService.RabbitMq.Options
{
    public class RabbitOptions
    {
        public const string RabbitData = "RabbitData";

        public string HostName { get; set; }

        public int Port { get; set; }

        public string LockQueue { get; set; }

        public string UnlockQueue { get; set; }

        public string LockExchange { get; set; }

        public string UnlockExchange { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
