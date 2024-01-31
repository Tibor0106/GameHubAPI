
namespace SteamV2Webapi.Objects
{
    public class Shop
    {
        public int Id { get; set; }
        public int gameId { get; set; }
        public int publisherId { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public int popularity { get; set; }
        public bool featured { get; set; }
    }
}
