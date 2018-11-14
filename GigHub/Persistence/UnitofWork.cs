using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigsRepository Gigs { get; private set; }

        public IAttendanceRepository Attendances { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IFollowingRepository Followings { get; private set; }

        public INotificationsRepository Notifications { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigsRepository(context);
            Attendances = new AttendanceRepository(context);
            Genres = new GenreRepository(context);
            Followings = new FollowingRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}