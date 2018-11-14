using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Core.Repositories;

namespace GigHub.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class CancelGigsController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public CancelGigsController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var gig = _iUnitOfWork.Gigs.GetGigWithAttendee(id);

            if (gig.ArtistId != User.Identity.GetUserId())
                return Unauthorized();

            if (gig.IsCanceled)
                return NotFound();

            gig.Cancel();

            _iUnitOfWork.Complete();

            return Ok();
        }
    }
}
