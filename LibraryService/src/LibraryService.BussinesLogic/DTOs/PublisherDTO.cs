using LibrarySevice.BussinesLogic.DTOs.Abstract;

namespace LibrarySevice.BussinesLogic.DTOs
{
    public class PublisherDTO : BaseDTO
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public IList<BookDTO> Books { get; set; }
    }
}
