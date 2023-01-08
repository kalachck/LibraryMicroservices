using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Entities
{
    public class AuthorEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public IList<BookAuthorEntity> BookAuthors { get; set; }
    }
}
