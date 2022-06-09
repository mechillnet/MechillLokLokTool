using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class PermissionRecord
    {
        public PermissionRecord()
        {
            PermissionRecordMapping = new HashSet<PermissionRecordMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SystemName { get; set; }
        public string Category { get; set; }
        public int? Order { get; set; }
        public string Url { get; set; }

        public virtual ICollection<PermissionRecordMapping> PermissionRecordMapping { get; set; }
    }
}
