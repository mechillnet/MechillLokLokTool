using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Picture
    {
        public int Id { get; set; }
        public string MimeType { get; set; }
        public int? Type { get; set; }
        public string SeoFilename { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribute { get; set; }
        public string VirtualPath { get; set; }

        public virtual PictureBinary PictureBinary { get; set; }
    }
}
