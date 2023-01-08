using LibrarySevice.BussinesLogic.DTOs.Abstract;

namespace LibrarySevice.BussinesLogic.DTOs
{
    public class BookDTO : BaseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; }

        public int PageCount { get; set; }

        public int PublisherId { get; set; }

        public PublisherDTO Publisher { get; set; }

        public IList<BookAuthorDTO> BookAuthors { get; set; }
    }
}
