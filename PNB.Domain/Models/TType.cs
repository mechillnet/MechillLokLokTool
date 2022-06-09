using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class TType
    {
        public TType()
        {
            TTicket = new HashSet<TTicket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorHex { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }

        public virtual ICollection<TTicket> TTicket { get; set; }
    }
}
