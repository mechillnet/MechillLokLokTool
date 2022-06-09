using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.OptionSelect
{
    public class SearchOptionSelectModel
    {
        public string SearchText { get; set; }

        public string ClassificationCode { get; set; }
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
    }
}
