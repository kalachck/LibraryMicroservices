namespace LibraryService.Api.Models
{
    public class GenreRequestModel
    {
        public string Name { get; set; }

        public IList<BookRequestModel> Books { get; set; }
    }
}
