using LibrarySevice.Api.Models.Abstract;

namespace LibrarySevice.Api.Models
{
    public class BookModel : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; }

        public int PageCount { get; set; }

        public int PublisherId { get; set; }

        public PublisherModel Publisher { get; set; }

        public IList<BookAuthorModel> BookAuthors { get; set; }
    }
}
