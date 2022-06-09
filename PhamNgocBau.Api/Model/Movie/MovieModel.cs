using Microsoft.AspNetCore.Http;

namespace PhamNgocBau.Api.Model.Movie
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OtherName { get; set; }
        public string Description { get; set; }
        public int? EpisodesTotal { get; set; }
        public int? Status { get; set; }
        public string StatusTitle { get; set; }
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
        public bool? IsPublish { get; set; }
        public bool? IsNew { get; set; }
        public string ShowTimes { get; set; }
        public IFormFile Avatar { get; set; }
        public IFormFile Panner { get; set; }
        public int? DisplayOrder { get; set; }
        public string SearchText { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string CastMapping { get; set; }
        public string CategoryMapping { get; set; }
        public bool? LokLokMovie { get; set; }
        public int? LokLokMovieId { get; set; }
        public int? LokLokCategoryId { get; set; }
    }
}
