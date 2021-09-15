using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class TransactionItem
    {
        public long Id { get; set; }
        public long BudgetId { get; set; }
        public long CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public long TransactionHeaderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Category Category { get; set; }
        public virtual TransactionHeader TransactionHeader { get; set; }
    }
}
