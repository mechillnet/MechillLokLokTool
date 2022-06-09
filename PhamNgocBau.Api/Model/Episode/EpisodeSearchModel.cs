using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Episode
{
    public class EpisodeSearchModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public int ProductId { get; set; }
        public int? EpisodeNumber { get; set; }
        public int? Type { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
