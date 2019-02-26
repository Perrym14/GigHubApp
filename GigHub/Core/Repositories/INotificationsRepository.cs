using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface INotificationsRepository
    {
        IEnumerable<Notification> GetNewNotificationsFor(string userId);

    }
}