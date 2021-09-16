using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class ResourcePolicy : AccessibleResource
    {
        public ResourcePolicy()
        {
            PrincipleResourcePolicies = new HashSet<PrincipleResourcePolicy>();
        }

        public string ResourceName { get; set; }
        public string ResourceAction { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<PrincipleResourcePolicy> PrincipleResourcePolicies { get; set; }
    }
}
