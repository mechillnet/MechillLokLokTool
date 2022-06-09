using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class CastMapping
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CastId { get; set; }

        public virtual Cast Cast { get; set; }
        public virtual Movie Product { get; set; }
    }
}
