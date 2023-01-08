using LibrarySevice.Api.Models.Abstract;

namespace LibrarySevice.Api.Models
{
    public class AuthorModel : BaseModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public IList<BookAuthorModel> BookAuthors { get; set; }
    }
}
