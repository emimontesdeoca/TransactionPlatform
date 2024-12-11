namespace TransactionPlatform.Client
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public DateTime When { get; set; }
        public string Where { get; set; }
        public string Who { get; set; }
        public string Shop { get; set; }

        public Transaction() { }

        public Transaction(decimal amount, DateTime when, string where, string who, string shop)
        {
            Amount = amount;
            When = when;
            Where = where;
            Who = who;
            Shop = shop;
        }

        public override string ToString()
        {
            return $"Transaction{{ Amount={Amount}, When={When}, Where={Where}, Who={Who}, Shop={Shop} }}";
        }
    }
}
