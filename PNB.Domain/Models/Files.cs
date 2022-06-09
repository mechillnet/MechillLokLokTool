using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Files
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string UrlLocal { get; set; }
        public string UrlExternal { get; set; }
        public string Password { get; set; }
        public long? Size { get; set; }
        public string Extension { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
