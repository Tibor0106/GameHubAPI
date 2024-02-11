namespace GameHubAPI.Objects
{
    public class Notification
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int notificationType { get; set; }
        public string notificationIcon { get; set; }
        public int notificationExtra { get; set; }
        public string notificationText { get; set; }
        public DateTime notificationTime { get; set; }
        public int read { get; set; }
        public Notification(int id, int userId, int notificationType, string notificationIcon, int notificationExtra, string notificationText, DateTime notificationTime) {
            this.id = id;
            this.userId = userId;
            this.notificationType = notificationType;
            this.notificationIcon = notificationIcon;
            this.notificationExtra = notificationExtra;
            this.notificationText = notificationText;
            this.notificationTime = notificationTime;
            read = 0;
        }
    }
}
