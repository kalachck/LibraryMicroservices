using LibrarySevice.BussinesLogic.Enums;

namespace LibrarySevice.BussinesLogic
{
    public class RabbitMessage
    {
        public Topic Topic { get; set; }

        public string Message { get; set; }
    }
}
