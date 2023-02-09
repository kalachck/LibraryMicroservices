using LibrarySevice.BussinesLogic.Enums;

namespace LibrarySevice.BussinesLogic
{
    public class RabbitMessage
    {
        public Topic Topic { get; set; }

        public int BookId { get; set; }
    }
}
