using GigHub.Core.ViewModels;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.Repositories;

namespace GigHub.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _iUnitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _iUnitOfWork.Gigs.GetUpcomingGigs();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g =>
                        g.Artist.Name.Contains(query) ||
                        g.Venue.Contains(query) ||
                        g.Genre.Name.Contains(query));
            }

            var userId = User.Identity.GetUserId();
            var attendances = _iUnitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };
            return View("Gigs", viewModel);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}