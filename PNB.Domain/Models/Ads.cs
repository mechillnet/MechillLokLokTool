using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Ads
    {
        public int Id { get; set; }
        public string FacebookVideoId { get; set; }
        public string Link { get; set; }
        public DateTime? ExpriesDate { get; set; }
    }
}
