using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Friend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string FacebookLink { get; set; }
        public string Zalo { get; set; }
        public string Avatar { get; set; }
        public int? GalleryImageId { get; set; }
    }
}
