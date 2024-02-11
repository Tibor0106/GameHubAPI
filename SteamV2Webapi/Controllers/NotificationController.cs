using Microsoft.AspNetCore.Mvc;
using PTHUWEBAPI.Database;
using GameHubAPI.Objects;
using GameHubAPI.DTO.Notification;

namespace GameHubAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public NotificationController(AppDbContext appDbcontext)
        {
            _appDbContext = appDbcontext;
        }

        [HttpPut]
        [Route("AddNotification")]
        public async Task<IActionResult> addNotification(AddNotificationDTO anDTO)
        {
            _appDbContext.notifications.Add(new Notification(0, anDTO.userId, anDTO.notificationType, anDTO.notificationIcon, anDTO.notificationExtra, anDTO.notificationText, DateTime.Now));
            await _appDbContext.SaveChangesAsync();
            return Ok(true);
        }
        [HttpGet]
        [Route("GetNotifications/{userId}")]
        public async Task<IActionResult> getNotification(int userId)
        {   
            var messagenotifications = (from n in _appDbContext.notifications
                                        join u in _appDbContext.users on n.userId equals u.Id
                                        select new { notification = n, user = u }).Where(i => i.user.Id == userId).ToList().OrderByDescending(i => i.notification.notificationTime.Ticks);
            return Ok(messagenotifications);
        }
        [HttpGet]
        [Route("ReadNotifications/{userId}")]
        public async Task<IActionResult> markNotificationsAsRead(int userId)
        {
            var notifications = _appDbContext.notifications.Where(i => i.userId == userId && i.read == 0).ToList();
            if (notifications == null)
                return BadRequest();
            for (int k = 0; k < notifications.Count; k++)
                notifications[k].read = 1;
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("DeleteNotification")]
        public async Task<IActionResult> DeleteNotification(int userId)
        {
            var notifications = _appDbContext.notifications.Where(i => i.userId == userId).ToList();
            for(int k = 0;k < notifications.Count;  k++)
                _appDbContext.notifications.Remove(notifications[k]);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
