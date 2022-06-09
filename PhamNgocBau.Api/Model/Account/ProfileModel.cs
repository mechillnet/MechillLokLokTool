using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Account
{
    public class ProfileModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
