namespace BorrowService.RabbitMq
{
    public class RabbitMessage
    {
        public int Id { get; set; }

        public Enums.Action Action { get; set; }
    }
}
