﻿using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigsRepository : IGigsRepository
    {
        private readonly IApplicationDbContext _context;

        public GigsRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetUserUpcomingGigs(string userId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now &&
                    !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }



        public Gig GetGigWithAttendee(int gigId)
        {
            return _context.Gigs
                 .Include(g => g.Attendances.Select(a => a.Attendee))
                 .SingleOrDefault(g => g.Id == gigId);
        }



        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigs()
        {
            return _context.Gigs
                 .Include(g => g.Artist)
                 .Include(g => g.Genre)
                 .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
        }
    }
}