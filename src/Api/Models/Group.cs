using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupCategories = new HashSet<GroupCategory>();
        }

        public long Id { get; set; }
        public long BudgetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Account Account { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual ICollection<GroupCategory> GroupCategories { get; set; }
    }
}
