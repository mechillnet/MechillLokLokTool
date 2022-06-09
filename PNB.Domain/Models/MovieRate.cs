using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class MovieRate
    {
        public int Id { get; set; }
        public int Point { get; set; }
        public int ProductId { get; set; }
        public string Ip { get; set; }
        public DateTime? CreateOn { get; set; }

        public virtual Movie Product { get; set; }
    }
}
