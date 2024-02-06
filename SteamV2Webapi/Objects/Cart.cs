namespace GameHubAPI.Objects
{
    public class Cart
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int gameId { get; set; }
        public Cart(int Id, int userId, int gameId)
        {
            this.Id = Id;
            this.userId = userId;
            this.gameId = gameId;
        }
    }
}
