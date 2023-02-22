namespace LibrarySevice.BussinesLogic.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int? AuthorId { get; set; } = null;

        public AuthorDTO Author { get; set; }

        public int? GenreId { get; set; } = null;

        public GenreDTO Genre { get; set; }

        public int? PublisherId { get; set; } = null;

        public PublisherDTO Publisher { get; set; }
    }
}
