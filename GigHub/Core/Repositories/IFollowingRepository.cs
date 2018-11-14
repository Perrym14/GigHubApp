using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string artistId);
        IEnumerable<ApplicationUser> GetFollowees(string userId);
        void AddFollowing(Following following);
        void RemoveFollowing(Following following);
        bool AnyFollowing(string userId, string followeeId);
    }
}