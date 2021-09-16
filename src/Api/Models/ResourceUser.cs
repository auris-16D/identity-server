using System;

#nullable disable

namespace Api.Models
{
    public partial class ResourceUser : AccessibleResource
    {
        public string PrincipleGuid { get; set; }
        public long ResourceId { get; set; }
        public string ResourceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
    }
}
