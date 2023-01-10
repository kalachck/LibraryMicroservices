using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.DataAccess.Repositories
{
    public class GenreRepository : BaseRepository<GenreEntity, ApplicationContext>
    {
        public GenreRepository(ApplicationContext context) : base(context)
        { }
    }
}
