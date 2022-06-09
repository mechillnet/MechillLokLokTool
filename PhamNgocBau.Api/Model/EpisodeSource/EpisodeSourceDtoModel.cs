using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.EpisodeSource
{
    public class EpisodeSourceDtoModel
    {
        public int Id { get; set; }
        public int ProductEpisodeId { get; set; }
        public int? SupplierId { get; set; }
        public string SubLink { get; set; }
        public string Link { get; set; }
        public bool? IsIframe { get; set; }
    }
}
