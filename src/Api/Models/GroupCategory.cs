using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class GroupCategory : AccessibleResource
    {
        public long CategoryId { get; set; }
        public long GroupId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Category Category { get; set; }
        public virtual Group Group { get; set; }

        public override bool IsParentOwnedBy(Guid principleId)
        {
            return this.Group.IsOwnedBy(principleId);
        }
    }
}
