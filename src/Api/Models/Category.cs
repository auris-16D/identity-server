using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Category : AccessibleResource
    {
        public Category()
        {
            GroupCategories = new HashSet<GroupCategory>();
            TransactionItems = new HashSet<TransactionItem>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<GroupCategory> GroupCategories { get; set; }
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
