using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Setting
    {
        public long Id { get; set; }
        public string PrincipalId { get; set; }
        public long ResourceId { get; set; }
        public string ResourceType { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Principal Principal { get; set; }
    }
}
