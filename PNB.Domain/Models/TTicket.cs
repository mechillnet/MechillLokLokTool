using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class TTicket
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateOpen { get; set; }
        public int? OpenBy { get; set; }
        public DateTime? DateClose { get; set; }
        public int? CloseBy { get; set; }
        public DateTime? DeadLine { get; set; }
        public int? StatusId { get; set; }
        public int? TypeId { get; set; }
        public int? PriorityId { get; set; }
        public int? SeverityId { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }

        public virtual TCategory Category { get; set; }
        public virtual TPriority Priority { get; set; }
        public virtual TStatus Status { get; set; }
        public virtual TType Type { get; set; }
    }
}
