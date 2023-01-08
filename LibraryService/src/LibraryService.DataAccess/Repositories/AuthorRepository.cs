using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.DataAccess.Repositories
{
    public class AuthorRepository : BaseRepository<AuthorEntity, ApplicationContext>
    {
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext)
        { }
    }
}
