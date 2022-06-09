using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class CategoryMapping
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Movie Product { get; set; }
    }
}
