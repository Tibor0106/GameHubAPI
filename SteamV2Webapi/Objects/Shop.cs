namespace Steam2WebApi.Objects;
{
    public class Shop
    {
        public int shopId {  get; set; }
        public int gameId {  get; set; }
        public int publisherId {  get; set; }
        public int price {  get; set; }
        public int discount {  get; set; }
        public int popularity {  get; set; }
        public bool featured { get; set; }
    }
}