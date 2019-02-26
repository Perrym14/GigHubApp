using AutoMapper;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NotificationsController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {

            var userId = User.Identity.GetUserId();
            var notifications = _iUnitOfWork.Notifications.GetNewNotificationsFor(userId);
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);


        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications =
                _iUnitOfWork.UserNotifications.GetUserNotifications(userId);

            notifications.ForEach(n => n.Read());

            _iUnitOfWork.Complete();

            return Ok();

        }
    }
}
