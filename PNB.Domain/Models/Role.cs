using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Role
    {
        public Role()
        {
            PermissionRecordMapping = new HashSet<PermissionRecordMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PermissionRecordMapping> PermissionRecordMapping { get; set; }
    }
}
