namespace LibrarySevice.BussinesLogic.DTOs
{
    public class GenreDTO
    {
        public string Title { get; set; }

        public IList<BookDTO> Books { get; set; }
    }
}
