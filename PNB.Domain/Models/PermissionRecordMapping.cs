using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class PermissionRecordMapping
    {
        public int PermissionRecordId { get; set; }
        public int CustomerRoleId { get; set; }

        public virtual Role CustomerRole { get; set; }
        public virtual PermissionRecord PermissionRecord { get; set; }
    }
}
