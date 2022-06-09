using System;

namespace PhamNgocBau.Api.Model.Cast
{
    public class CastDtoModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string SearchText { get; set; }
        public string Link { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
    }
}
