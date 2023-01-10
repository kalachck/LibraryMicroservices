namespace LibrarySevice.Api.Models
{
    public class BookRequestModel
    {
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public int AuthorId { get; set; }

        public AuthorRequestModel Author { get; set; }

        public int GenreId { get; set; }

        public GenreRequestModel Genre { get; set; }

        public int PublisherId { get; set; }

        public PublisherRequestModel Publisher { get; set; }
    }
}
