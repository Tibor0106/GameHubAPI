namespace SteamV2Webapi.DTO.GameStat
{
    public class AddGameLevelsDTO
    {
        public int userId { get; set; }
        public int gameId { get; set; }
        public int toAddLevels { get; set; }
    }
}
