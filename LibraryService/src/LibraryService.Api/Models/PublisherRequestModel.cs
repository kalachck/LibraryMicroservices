namespace LibraryService.Api.Models
{
    public class PublisherRequestModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public IList<BookRequestModel> Books { get; set; }
    }
}
