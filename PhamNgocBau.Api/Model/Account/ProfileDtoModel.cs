using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Account
{
    public class ProfileDtoModel
    {
        public string username { get; set; }
        public string name { get; set; }
        public string address { get; set; }

        public string phone { get; set; }

        public bool? gender { get; set; }
        public string email { get; set; }
        public string image { get; set; }
        public string money { get; set; }
    }
}
