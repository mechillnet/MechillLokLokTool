using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
    }
}
