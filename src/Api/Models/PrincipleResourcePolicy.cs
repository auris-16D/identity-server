using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class PrincipleResourcePolicy
    {
        public long Id { get; set; }
        public long BudgetId { get; set; }
        public long ResourcePolicyId { get; set; }
        public string PrincipleGuid { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ResourcePolicy ResourcePolicy { get; set; }
    }
}
