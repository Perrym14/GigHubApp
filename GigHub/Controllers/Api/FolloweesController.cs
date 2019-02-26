using GigHub.Core.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace GigHub.Controllers.Api
{
    public class FolloweesController : Controller
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public FolloweesController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artists = _iUnitOfWork.Users.GetArtistsFollowedBy(userId);

            return View(artists);
        }

    }
}