using GigHub.Core.Repositories;

namespace GigHub.Persistence
{
    public interface IUnitOfWork
    {
        IGigsRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        void Complete();
    }
}