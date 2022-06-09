using System;
using System.Collections.Generic;

namespace PNB.Domain.Models
{
    public partial class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string FlagImageFileName { get; set; }
        public string Currency { get; set; }
        public bool? Published { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
