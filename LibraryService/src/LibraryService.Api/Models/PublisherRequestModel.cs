using LibraryService.BussinesLogic.Validators;

namespace LibraryService.Api.Models
{
    public class PublisherRequestModel
    {
        [NotNullOrEmpty]
        [HasMaxLength(30)]
        public string Name { get; set; }

        [NotNullOrEmpty]
        [HasMaxLength(30)]
        public string Address { get; set; }

        public IList<BookRequestModel> Books { get; set; }
    }
}
