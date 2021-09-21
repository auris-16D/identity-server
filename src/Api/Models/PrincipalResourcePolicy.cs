using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class PrincipalResourcePolicy : AccessibleResource
    {
        public long ResourcePolicyId { get; set; }
        public string PrincipalId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Principal Principal { get; set; }
        public virtual ResourcePolicy ResourcePolicy { get; set; }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
