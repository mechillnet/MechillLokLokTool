using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Country
{
    public class CountrySearchModel:DataTableSearch
    {
   
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
