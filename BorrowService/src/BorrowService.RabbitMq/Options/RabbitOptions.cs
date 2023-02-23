namespace BorrowService.RabbitMq.Options
{
    public class RabbitOptions
    {
        public const string RabbitData = "RabbitData";

        public string HostName { get; set; }

        public int Port { get; set; }

        public string Queue { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
