using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string DomainName { get; set; }
        public string Noticafition { get; set; }
    }
}
