using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Topic
    {
        public int Id { get; set; }
        public bool? IncludeInTopMenu { get; set; }
        public bool? Password { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Script { get; set; }
        public string Css { get; set; }
        public bool Published { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public int? Order { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
