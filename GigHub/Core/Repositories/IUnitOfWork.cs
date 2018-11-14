namespace GigHub.Core.Repositories
{
    public interface IUnitOfWork
    {
        IGigsRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        INotificationsRepository Notifications { get; }
        void Complete();
    }
}