namespace GameHubAPI.DTO.Notification
{
    public class AddNotificationDTO
    {
        public int notificationType { get; set; }
        public int userId { get; set; }
        public string notificationIcon { get; set; }
        public int notificationExtra { get; set; }
        public string notificationText { get; set; }
    }
}
