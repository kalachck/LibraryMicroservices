namespace LibrarySevice.Api.Models
{
    public class GenreRequestModel
    {
        public string Title { get; set; }

        public IList<BookRequestModel> Books { get; set; }
    }
}
