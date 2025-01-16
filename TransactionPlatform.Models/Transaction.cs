using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TransactionPlatform.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CreatedIn { get; set; }
        public int ShopId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Item> Items { get; set; }
        public int PaymentTypeId { get; set; }
        public bool Financed { get; set; }
        public int FinancedMonths { get; set; }

        public Transaction()
        {
            Items = new List<Item>();
        }
    }
}