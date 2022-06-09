using PhamNgocBau.Api.Model.Cast;
using PhamNgocBau.Api.Model.Category;
using PhamNgocBau.Api.Model.Country;
using PhamNgocBau.Api.Model.Episode;
using PhamNgocBau.Api.Model.Movie.Product;
using System.Collections.Generic;

namespace PhamNgocBau.Api.Model.Movie
{
    public class MovieDtoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OtherName { get; set; }
        public string Description { get; set; }
        public string StatusTitle { get; set; }
        public string PlayMovie { get; set; }
        public string Keyword { get; set; }
        public string SeoKeywords { get; set; }

        public string OriginalLink { get; set; }
        public int? EpisodesTotal { get; set; }

        public int? EpisodeCurrentVietSub { get; set; }
        public int? EpisodeCurrentThuyetMinh { get; set; }
        public string Status { get; set; }
        public string Year { get; set; }
        public string Language { get; set; }
        public CountryDtoModel Country { get; set; }

        public string Cast { get; set; }
        public string Director { get; set; }
        public string Time { get; set; }
        public string Trailer { get; set; }
        public bool? ShowTop { get; set; }
        public bool? ShowCenter { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsPublish { get; set; }
        public string Link { get; set; }
        public int? TypeId { get; set; }
        public string ShowTimes { get; set; }
        public string Avatar { get; set; }
        public string Panner { get; set; }
        public int? DisplayOrder { get; set; }
        public string SearchText { get; set; }
        public string SeoTitle { get; set; }
        public int? RateNumner { get; set; }
        public int? RatePoint { get; set; }
        public int? LikeNumber { get; set; }
        public string SeoDescription { get; set; }
        public string NextEpisode { get; set; }
        public string OgImage { get; set; }
        public virtual ICollection<EpisodeDtoModel> Episode { get; set; }
        public virtual IList<EpisodeDtoModel> EpisodeVietSub { get; set; }
        public virtual IList<EpisodeDtoModel> EpisodePreview { get; set; }
        public virtual IList<EpisodeDtoModel> EpisodeThuyetMinh { get; set; }
        public virtual ICollection<CastMappingModel> CastMapping { get; set; }
        public virtual ICollection<CategoryMappingModel> CategoryMapping { get; set; }
        public bool? LokLokMovie { get; set; }
        public int? LokLokMovieId { get; set; }
        public int? LokLokCategoryId { get; set; }
    }
}
