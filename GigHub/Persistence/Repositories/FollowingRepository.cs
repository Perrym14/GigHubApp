using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string userId, string artistId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == artistId);
        }

        public IEnumerable<ApplicationUser> GetFollowees(string userId)
        {
            return _context.Followings.Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }

        public void AddFollowing(Following following)
        {
            _context.Followings.Add(following);
        }

        public void RemoveFollowing(Following following)
        {
            _context.Followings.Remove(following);
        }

        public bool AnyFollowing(string userId, string followeeId)
        {
            return _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followeeId);
        }

    }


}