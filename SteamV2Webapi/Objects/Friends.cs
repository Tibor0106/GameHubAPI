namespace SteamV2Webapi.Objects
{
    public class Friends
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int friendId { get; set; }
        public DateTime friend_since { get; set; }
    }
}
