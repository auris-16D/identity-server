using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Principal
    {
        public Principal()
        {
            PrincipalResourcePolicies = new HashSet<PrincipalResourcePolicy>();
            Reconciliations = new HashSet<Reconciliation>();
            ResourceUsers = new HashSet<ResourceUser>();
            Settings = new HashSet<Setting>();
            TransactionHeaders = new HashSet<TransactionHeader>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<PrincipalResourcePolicy> PrincipalResourcePolicies { get; set; }
        public virtual ICollection<Reconciliation> Reconciliations { get; set; }
        public virtual ICollection<ResourceUser> ResourceUsers { get; set; }
        public virtual ICollection<Setting> Settings { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }
    }
}
