namespace LibrarySevice.BussinesLogic.DTOs
{
    public class AuthorDTO
    {
        public string Name { get; set; }

        public IList<BookDTO> Books { get; set; }
    }
}
