using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _iUnitOfWork = unitOfWork;
        }


        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _iUnitOfWork.Attendances.GetAttendance(userId, dto.GigId);
            if (attendance != null)
                return BadRequest("The attendance already exists");

            attendance = new Attendance
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
