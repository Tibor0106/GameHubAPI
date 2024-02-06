namespace GameHubAPI.DTO.Transaction
{
    public class TransactionDTO
    {

        public string transactionData { get; set; } // NEM LEHET DICTIONARY, jó lesz az stringként jsben
                                                    // úgyis parseolható
        public int price { get; set; }
    }
}
