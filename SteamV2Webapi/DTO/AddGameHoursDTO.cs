namespace SteamV2Webapi.DTO
{
    public class AddGameHoursDTO
    {
        public int userId {  get; set; }
        public int gameId { get; set; }
        public int toAddHours {  get; set; }
    }
}
