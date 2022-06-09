using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class HistoryLink
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public int EntityId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateBy { get; set; }
    }
}
