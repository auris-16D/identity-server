using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Category
    {
        public Category()
        {
            GroupCategories = new HashSet<GroupCategory>();
            TransactionItems = new HashSet<TransactionItem>();
        }

        public long Id { get; set; }
        public long BudgetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<GroupCategory> GroupCategories { get; set; }
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
