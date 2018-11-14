using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Core.Repositories;

namespace GigHub.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _iUnitOfWork = unitOfWork;
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_iUnitOfWork.Attendances.AnyAttendances(userId, dto.GigId))
            {
                return BadRequest("The attendance already exists");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _iUnitOfWork.Attendances.AddAttendance(attendance);
            _iUnitOfWork.Complete();
            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult CancelAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _iUnitOfWork.Attendances.GetAttendance(userId, id);

            if (attendance == null)
                return NotFound();

            _iUnitOfWork.Attendances.RemoveAttendance(attendance);
            _iUnitOfWork.Complete();
            return Ok(id);
        }
    }
}
