using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class MyContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IsUser { get; set; }
        public string Facebook { get; set; }
        public string Avatar { get; set; }
    }
}
