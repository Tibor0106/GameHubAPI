namespace SteamV2Webapi.Objects
{
    public class Cart
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int gameId { get; set; }
        public Cart(int userId, int gameId)
        {
            this.userId = userId;
            this.gameId = gameId;
        }
    }
}
