using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.LokLok
{
    public class responseMovie
    {
        public string code { get; set; }
        public MovieDetailLokLok data { get; set; }
    }
    public class MovieDetailLokLok
    {
        public int id { get; set; }
        public int? category { get; set; }
        public string coverHorizontalUrl { get; set; }
        public string coverVerticalUrl { get; set; }
        public int? episodeCount { get; set; }
        public string introduction { get; set; }
        public string name { get; set; }
        public double? score { get; set; }
        public List<episodeVo> episodeVo { get; set; }
        public int? year { get; set; }
        public List<TagList> tagList { get; set; }
    }
    public class TagList
    {

       public int? id { get; set; }
        public string name { get; set; }
    }
    public class episodeVo
    {
        public int id { get; set; }
        public int? seriesNo { get; set; }
        public List<definitionList> definitionList { get; set; }
    }
    public class definitionList
    {
        public string code { get; set; }
    }
}
