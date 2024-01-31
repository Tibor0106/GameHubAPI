namespace Steam2WebApi.Objects;
{
    public class Transactions
    {
        public int transactionId {  get; set; }
        public Dictionary<string, string> transactionData { get; set; }
        public int price { get; set; }
    }
}