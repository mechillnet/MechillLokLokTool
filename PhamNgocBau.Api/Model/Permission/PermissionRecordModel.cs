using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Permission
{
    public class PermissionRecordModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string Category { get; set; }

        public string Url { get; set; }
        public bool Enable { get; set; }
    }
}
