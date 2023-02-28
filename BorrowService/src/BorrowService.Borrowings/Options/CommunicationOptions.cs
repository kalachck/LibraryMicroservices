namespace BorrowService.Borrowings.Options
{
    public class CommunicationOptions
    {
        public const string CommunicationUrls = "CommunicationUrls";

        public string Identity { get; set; }

        public string LibraryByTitle { get; set; }

        public string LibraryById { get; set; }
    }
}
