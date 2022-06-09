using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Permission
{
    public class PermissionRequestModel
    {
        public int roleId { get; set; }
        public IDictionary<string, string> data { get; set; }
    }
}
