using LibrarySevice.Api.Models.Abstract;

namespace LibrarySevice.Api.Models
{
    public class PublisherModel : BaseModel
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public IList<BookModel> Books { get; set; }
    }
}
