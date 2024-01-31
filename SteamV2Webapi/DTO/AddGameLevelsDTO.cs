namespace SteamV2Webapi.DTO
{
    public class AddGameLevelsDTO
    {
        public int userId {  get; set; }
        public int gameId { get; set; }
        public int toAddLevels {  get; set; }
    }
}
