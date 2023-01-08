using LibrarySevice.BussinesLogic.DTOs.Abstract;

namespace LibrarySevice.BussinesLogic.DTOs
{
    public class AuthorDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public IList<BookAuthorDTO> BookAuthors { get; set; }
    }
}
