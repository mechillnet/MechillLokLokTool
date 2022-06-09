using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.LokLok
{
    public class MovieMedia
    {
        public string code { get; set; }
        public MovieMediaData data { get; set; }
    }
    public class MovieMediaData
    {
        public int? totalDuration { get; set; }
    } 
}
