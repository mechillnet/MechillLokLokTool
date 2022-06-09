using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Admin.Picture
{
    public class PictureDtoModel
    {
        public int Id { get; set; }
        public int? DisplayOrder { get; set; }
        public string MimeType { get; set; }
        public string SeoFilename { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribute { get; set; }
        public string VirtualPath { get; set; }
    }
}
