using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Episode
    {
        public Episode()
        {
            EpisodeSource = new HashSet<EpisodeSource>();
        }

        public int Id { get; set; }
        public int EpisodeNumber { get; set; }
        public bool? Status { get; set; }
        public int ProductId { get; set; }
        public int? ViewNumber { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int? UpdateBy { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string FullLink { get; set; }
        public int? Type { get; set; }
        public string Keyword { get; set; }

        public virtual Movie Product { get; set; }
        public virtual ICollection<EpisodeSource> EpisodeSource { get; set; }
    }
}
