using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class ResourceUser : AccessibleResource
    {
        public string PrincipleId { get; set; }
        public long ResourceId { get; set; }
        public string ResourceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Principle Principle { get; set; }
    }
}
