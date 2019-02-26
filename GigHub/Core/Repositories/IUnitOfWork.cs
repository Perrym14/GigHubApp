namespace GigHub.Core.Repositories
{
    public interface IUnitOfWork
    {
        IGigsRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        IApplicationUserRepository Users { get; }
        INotificationsRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }
        void Complete();
    }
}