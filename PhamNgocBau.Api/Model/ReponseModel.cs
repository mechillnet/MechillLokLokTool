using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model
{
    public partial class ReponseModel
    {
        public int code { get; set; }
       
        public int? info_card { get; set; }
        public string msg { get; set; }

    }
    public partial class ResponseApiBaseModel
    {
        public ResponseApiBaseModel()
        {
            data = new object();
        }
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
