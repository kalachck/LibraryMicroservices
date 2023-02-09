namespace LibrarySevice.Api.Models
{
    public class BookRequestModel
    {
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public bool? IsAvailable { get; set; } = true;

        public int? AuthorId { get; set; } = null;

        public int? GenreId { get; set; } = null;

        public int? PublisherId { get; set; } = null;
    }
}
