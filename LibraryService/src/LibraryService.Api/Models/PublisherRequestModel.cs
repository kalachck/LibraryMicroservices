namespace LibrarySevice.Api.Models
{
    public class PublisherRequestModel
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public IList<BookRequestModel> Books { get; set; }
    }
}
