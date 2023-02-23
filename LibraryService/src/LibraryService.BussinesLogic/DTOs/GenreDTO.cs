namespace LibraryService.BussinesLogic.DTOs
{
    public class GenreDTO
    {
        public string Name { get; set; }

        public IList<BookDTO> Books { get; set; }
    }
}
