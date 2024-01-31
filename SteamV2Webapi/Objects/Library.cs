namespace SteamV2Webapi.Objects
{
    public class Library
    {
        public int userId {  get; set; }
        public int gameId { get; set; }
        public DateTime purchased_since { get; set; }

        public Library(int id, int userId, int gameId, DateTime purchased_since)
        {
            this.userId = userId;
            this.gameId = gameId;
            this.purchased_since = purchased_since;
        }
    }
}
