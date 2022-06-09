using PNB.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Domain.Settings
{
    public partial class CommonSetting : ISettings
    {
        public bool Maintenance { get; set; }
        public bool Enable_Rent { get; set; }
    }
}
