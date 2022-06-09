using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryMapping = new HashSet<CategoryMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int? DisplayOrder { get; set; }
        public int? DisplayColumn { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }

        public virtual ICollection<CategoryMapping> CategoryMapping { get; set; }
    }
}
