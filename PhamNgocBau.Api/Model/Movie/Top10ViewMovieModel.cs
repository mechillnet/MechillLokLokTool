using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Movie
{
    public partial class Top10ViewMovieModel
    {
        public int ViewNumber { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Avatar { get; set; }
    }
}
