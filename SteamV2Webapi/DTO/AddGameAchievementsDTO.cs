namespace SteamV2Webapi.DTO
{
    public class AddGameAchievementsDTO
    {
        public int userId {  get; set; }
        public int gameId { get; set; }
        public int toAddAchievements {  get; set; }
    }
}
