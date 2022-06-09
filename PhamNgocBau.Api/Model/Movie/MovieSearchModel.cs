using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Movie
{
    public class MovieSearchModel:DataTableSearch
    {
        public int? CategoryId { get; set; }
        public int? CountryId { get; set; }
        public bool? IsPublish { get; set; }
        public int? StatusId { get; set; }
        public int? TypeId { get; set; }
        public int? Year { get; set; }
        public bool? GetAnime { get; set; }
        public string OrderBy { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
