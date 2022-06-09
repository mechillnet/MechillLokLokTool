using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Account
{
    public partial class LoginReponseModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }
        public string Money { get; set; }
        public string Name { get; set; }
    }
}
