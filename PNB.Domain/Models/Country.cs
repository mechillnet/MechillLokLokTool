using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Country
    {
        public Country()
        {
            Movie = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int? DisplayOrder { get; set; }
        public int? DisplayColumn { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
    }
}
