using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Settings
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
