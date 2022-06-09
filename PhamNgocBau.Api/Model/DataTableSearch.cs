using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model
{
    public class DataTableSearch
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string Search { get; set; }
    }
}
