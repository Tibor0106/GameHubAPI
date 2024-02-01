namespace Steam2WebApi.Objects
{
    public class Transaction
    {
        public int Id {  get; set; }
        public string transactionData { get; set; }// Cannot bind 'transactionData' in 'Transaction(int id, Dictionary<string, string> transactionData, int price)'
        public int price { get; set; }
        public Transaction(string transactionData, int price)
        {
        
            this.transactionData = transactionData;
            this.price = price;
        }
    }
}

