using PhamNgocBau.Api.Model.EpisodeSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Episode
{
    public class EpisodeModel
    {
        public int? Id { get; set; }
        public int EpisodeNumber { get; set; }
        public bool? Status { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int ProductId { get; set; }
        public int? ViewNumber { get; set; }
        public  ICollection<EpisodeSourceModel> EpisodeSource { get; set; }
    }
}
