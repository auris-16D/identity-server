using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class ResourceUser : IAccessibleResource
    {
        public long Id { get; set; }
        public long BudgetId { get; set; }
        public string PrincipleGuid { get; set; }
        public long ResourceId { get; set; }
        public string ResourceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }

        public bool IsOwnedBy(Guid principleId)
        {
            return true;
        }

        public bool IsParentOwnedBy(Guid principleId)
        {
            return true;
        }
    }
}
