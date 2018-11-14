using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public Attendance GetAttendance(string userId, int gigId)
        {
            return _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == userId && a.GigId == gigId);
        }

        public void AddAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);

        }

        public void RemoveAttendance(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

        public bool AnyAttendances(string userId, int gigId)
        {
            return _context.Attendances
                .Any(a => a.AttendeeId == userId && a.GigId == gigId);
        }


    }


}