namespace Steam2WebApi.Objects
{
    public class Transaction
    {
        public int Id {  get; set; }
        public Dictionary<string, string> ?transactionData { get; set; }
        public int price { get; set; }
        public Transaction(int id, Dictionary<string, string>? transactionData, int price)
        {
            Id = id;
            this.transactionData = transactionData;
            this.price = price;
        }
    }
}

