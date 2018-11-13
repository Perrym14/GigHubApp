using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IGigsRepository
    {
        IEnumerable<Gig> GetUserUpcomingGigs(string userId);
        Gig GetGig(int gigId);
        void Add(Gig gig);
        Gig GetGigWithAttendee(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
    }
}