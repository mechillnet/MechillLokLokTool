using PNB.Domain.Configuration;

namespace PhamNgocBau.Api.Extension.ApiSetting
{
    public partial class ConfigurationApi:ISettings
    {
        public string merchant_id { get; set; }
        public string user_gamebank { get; set; }
        public string password_gamebank { get; set; }
        public bool enablesend { get; set; }
        public string Host { get; set; }
        public string DirectoryUpload { get; set; }
        public string Key { get; set; }
    }
}
