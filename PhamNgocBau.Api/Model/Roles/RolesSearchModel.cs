using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Roles
{
    public class RolesSearchModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
    }
}
