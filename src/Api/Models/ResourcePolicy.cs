using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class ResourcePolicy : AccessibleResource
    {
        public ResourcePolicy()
        {
            PrincipalResourcePolicies = new HashSet<PrincipalResourcePolicy>();
        }

        public string ResourceName { get; set; }
        public string ResourceAction { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<PrincipalResourcePolicy> PrincipalResourcePolicies { get; set; }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
