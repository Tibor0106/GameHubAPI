namespace SteamV2Webapi.Objects
{
    public class GameStats
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int gameId { get; set; }
        public int playerHours { get; set; }
        public int level { get; set; }
        public int achievements { get; set; }

    }
}
