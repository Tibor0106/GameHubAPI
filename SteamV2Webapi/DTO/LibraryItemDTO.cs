namespace SteamV2Webapi.DTO
{
    public class LibraryItemDTO
    {
        public int userId { get; set; }
        public int gameId { get; set; }
        public DateTime purchased_since { get; set; }
    }
}
