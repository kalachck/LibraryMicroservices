namespace LibraryService.Api.Models
{
    public class AuthorRequestModel
    {
        public string Name { get; set; }

        public IList<BookRequestModel> Books { get; set; }
    }
}
