using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class ResourceUser : AccessibleResource
    {
        public string PrincipalId { get; set; }
        public long ResourceId { get; set; }
        public string ResourceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Principal Principal { get; set; }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
