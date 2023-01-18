namespace BorrowService.Borrowings.Options
{
    public class MailOptions
    {
        public const string MailData = "MailData";

        public string SourceMail { get; set; }

        public string SourcePassword { get; set; }

        public string Subject { get; set; }
    }
}
