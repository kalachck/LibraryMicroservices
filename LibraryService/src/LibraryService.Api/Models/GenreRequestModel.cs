using LibraryService.BussinesLogic.Validators;

namespace LibraryService.Api.Models
{
    public class GenreRequestModel
    {
        [NotNullOrEmpty]
        [HasMaxLength(30)]
        public string Name { get; set; }

        public IList<BookRequestModel> Books { get; set; }
    }
}
