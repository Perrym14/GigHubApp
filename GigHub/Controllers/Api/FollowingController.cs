using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    public class FollowingController : ApiController
    {

        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Already following.");


            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {

            var userId = User.Identity.GetUserId();

            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null)
                return NotFound();

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(id);
        }
    }


}
