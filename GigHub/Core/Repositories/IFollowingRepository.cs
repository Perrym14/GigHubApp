using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string artistId);
        void AddFollowing(Following following);
        void RemoveFollowing(Following following);
    }
}