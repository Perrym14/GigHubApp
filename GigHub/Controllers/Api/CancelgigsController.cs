using GigHub.Core.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class CancelGigsController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CancelGigsController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }


        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = _iUnitOfWork.Gigs.GetGigWithAttendee(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            gig.Cancel();

            _iUnitOfWork.Complete();

            return Ok();
        }
    }
}
