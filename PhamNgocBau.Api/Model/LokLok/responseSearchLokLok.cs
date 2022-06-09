using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.LokLok
{
   public class responseModelLokLok
    {
        public string code { get; set; }
        public dataSearch data { get; set; }
    }
    public class dataSearch
    {
        public List<ListSearchModel> searchResults { get; set; }
       
    }
}
