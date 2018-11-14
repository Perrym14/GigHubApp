using AutoMapper;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Repositories;

namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public NotificationsController(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<NotificationDto> GetNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _iUnitOfWork.Notifications.GetUnreadNotifications(userId);

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);

        }


        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications =
                _iUnitOfWork.Notifications.GetUnreadUserNotifications(userId);

            notifications.ForEach(n => n.MarkRead());

            _iUnitOfWork.Complete();

            return Ok();

        }
    }
}
