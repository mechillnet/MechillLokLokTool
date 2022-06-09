using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Cast
    {
        public Cast()
        {
            CastMapping = new HashSet<CastMapping>();
        }

        public int Id { get; set; }
        public string Avatar { get; set; }
        public string AvatarThumb { get; set; }
        public int? AvatarId { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string SearchText { get; set; }
        public string Link { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public int? LikeNumber { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string CdnAvatar { get; set; }
        public string CdnAvatarThumb { get; set; }

        public virtual ICollection<CastMapping> CastMapping { get; set; }
    }
}
