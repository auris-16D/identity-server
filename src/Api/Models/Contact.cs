using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Contact
    {
        public Contact()
        {
            TransactionHeaders = new HashSet<TransactionHeader>();
        }

        public long Id { get; set; }
        public long BudgetId { get; set; }
        public long ContactTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }
    }
}
