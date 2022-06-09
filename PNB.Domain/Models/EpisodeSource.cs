using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class EpisodeSource
    {
        public int Id { get; set; }
        public int? ProductEpisodeId { get; set; }
        public int? SupplierId { get; set; }
        public string Link { get; set; }
        public string LinkTemp { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool? IsIframe { get; set; }
        public string VideoId { get; set; }
        public string SubLink { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int? UpdateBy { get; set; }

        public virtual Episode ProductEpisode { get; set; }
    }
}
