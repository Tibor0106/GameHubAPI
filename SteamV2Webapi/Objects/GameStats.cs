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

        public GameStats(int id, int userId, int gameId, int playerHours, int level, int achievements)
        {
            Id = id;
            this.userId = userId;
            this.gameId = gameId;
            this.playerHours = playerHours;
            this.level = level;
            this.achievements = achievements;
        }
    }
}
