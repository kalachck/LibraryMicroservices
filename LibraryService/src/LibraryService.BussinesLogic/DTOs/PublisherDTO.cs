namespace LibraryService.BussinesLogic.DTOs
{
    public class PublisherDTO
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public IList<BookDTO> Books { get; set; }
    }
}
