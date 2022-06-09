using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Movie
    {
        public Movie()
        {
            CastMapping = new HashSet<CastMapping>();
            CategoryMapping = new HashSet<CategoryMapping>();
            Episode = new HashSet<Episode>();
            MovieRate = new HashSet<MovieRate>();
            MovieView = new HashSet<MovieView>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string OtherName { get; set; }
        public string Avatar { get; set; }
        public int? AvatarId { get; set; }
        public string Panner { get; set; }
        public int? PannerId { get; set; }
        public string Description { get; set; }
        public int? EpisodesTotal { get; set; }
        public int? Status { get; set; }
        public int? Year { get; set; }
        public string Language { get; set; }
        public int? CountryId { get; set; }
        public string Director { get; set; }
        public string Time { get; set; }
        public string Trailer { get; set; }
        public string Link { get; set; }
        public int? TypeId { get; set; }
        public bool? ShowTop { get; set; }
        public bool? ShowCenter { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsPublish { get; set; }
        public string ShowTimes { get; set; }
        public string SearchText { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public int? UpdateBy { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public int? DisplayOrder { get; set; }
        public int? LikeNumber { get; set; }
        public int? ViewNumber { get; set; }
        public int? RatePoint { get; set; }
        public int? RateNumner { get; set; }
        public string Keyword { get; set; }
        public bool? LokLokMovie { get; set; }
        public int? LokLokMovieId { get; set; }
        public int? LokLokCategoryId { get; set; }
        public string StatusTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string Cdnavatar { get; set; }
        public string Cdnbanner { get; set; }
        public string OriginalLink { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<CastMapping> CastMapping { get; set; }
        public virtual ICollection<CategoryMapping> CategoryMapping { get; set; }
        public virtual ICollection<Episode> Episode { get; set; }
        public virtual ICollection<MovieRate> MovieRate { get; set; }
        public virtual ICollection<MovieView> MovieView { get; set; }
    }
}
