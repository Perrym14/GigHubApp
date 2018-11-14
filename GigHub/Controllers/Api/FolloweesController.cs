using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using GigHub.Core.Repositories;

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

            var artists = _iUnitOfWork.Followings.GetFollowees(userId);

            return View(artists);
        }

    }
}