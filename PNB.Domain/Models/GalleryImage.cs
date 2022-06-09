using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class GalleryImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }
        public int FriendId { get; set; }
    }
}
