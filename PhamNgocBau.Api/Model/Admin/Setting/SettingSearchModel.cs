using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Admin.Setting
{
    public class SettingSearchModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string Key { get; set; }
    }
}
