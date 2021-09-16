using System;

#nullable disable

namespace Api.Models
{
    public partial class PrincipleResourcePolicy : AccessibleResource
    {
        public long ResourcePolicyId { get; set; }
        public string PrincipleGuid { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ResourcePolicy ResourcePolicy { get; set; }
    }
}
