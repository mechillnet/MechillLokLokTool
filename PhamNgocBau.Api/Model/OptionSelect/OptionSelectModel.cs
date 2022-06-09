using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.OptionSelect
{
    public class OptionSelectModel
    {
        public int? Id { get; set; }
        public string ClassificationCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AttributesValue { get; set; }
        public string AttributesValue1 { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
