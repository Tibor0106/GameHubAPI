namespace SteamV2Webapi.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public Dictionary<string, string>? transactionData { get; set; }
        public int price { get; set; }
    }
}
