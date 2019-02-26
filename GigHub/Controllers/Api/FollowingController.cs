using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{

    public class FollowingController : ApiController
    {

        private readonly IUnitOfWork _iUnitOfWork;

        public FollowingController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var following = _iUnitOfWork.Followings.GetFollowing(userId, dto.FolloweeId);

            if (following != null)
            {
                return BadRequest("Following already exists");
            }
            following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _iUnitOfWork.Followings.AddFollowing(following);
            _iUnitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {

            var userId = User.Identity.GetUserId();

            var following = _iUnitOfWork.Followings.GetFollowing(userId, id);

            if (following == null)
                return NotFound();

            _iUnitOfWork.Followings.RemoveFollowing(following);
            _iUnitOfWork.Complete();

            return Ok(id);
        }
    }


}
