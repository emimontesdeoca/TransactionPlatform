using System.Text.Json.Serialization;

namespace TransactionPlatform.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public decimal Amount { get; set; }

        public Item() { }
    }
}