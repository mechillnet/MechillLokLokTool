using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class MovieView
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public DateTime? ViewAt { get; set; }

        public virtual Movie Product { get; set; }
    }
}
