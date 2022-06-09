using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class PermissionCategory
    {
        public int Id { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int? Order { get; set; }
    }
}
