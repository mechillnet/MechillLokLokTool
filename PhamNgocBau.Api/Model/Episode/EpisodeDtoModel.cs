using PhamNgocBau.Api.Model.EpisodeSource;
using PhamNgocBau.Api.Model.Movie;
using System.Collections.Generic;

namespace PhamNgocBau.Api.Model.Episode
{
    public class EpisodeDtoModel
    {
        public int Id { get; set; }
        public int EpisodeNumber { get; set; }
        public string Keyword { get; set; }
        public string SeoKeywords { get; set; }
        public bool? Status { get; set; }
        public string Name { get; set; }
        public string SeoName { get; set; }
        public string Link { get; set; }
        public string FullLink { get; set; }
        public int? Type { get; set; }
        public int ProductId { get; set; }
        public int? ViewNumber { get; set; }
        public ICollection<EpisodeSourceModel> EpisodeSource { get; set; }
    }
}
