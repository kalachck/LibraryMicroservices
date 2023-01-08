using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories.Abstract;

namespace LibrarySevice.DataAccess.Repositories
{
    public class PublisherRepository : BaseRepository<PublisherEntity, ApplicationContext>
    {
        public PublisherRepository(ApplicationContext context) : base(context)
        { }
    }
}
