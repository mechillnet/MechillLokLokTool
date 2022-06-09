using Newtonsoft.Json;
using PNB.Service.sSetting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PNB.Service.FacebookApi
{
   public class FacebookApiService :IFacebookApiService
    {
        private readonly ISettingService _settingService;
        private FacebookApiConfig _facebookconfig;
        public FacebookApiService(ISettingService settingService)
        {
            _settingService = settingService;
            _facebookconfig = _settingService.LoadSetting<FacebookApiConfig>();
        }
        public string GetSourceVideoById(string videoId)
        {
            Uri uri = new Uri(_facebookconfig.host);
            string result = "";
            using (var client = new HttpClient() { BaseAddress = uri })
            {
                var response = client.GetAsync(string.Format("v12.0/{0}?fields=source&access_token={1}", videoId, _facebookconfig.token));
                if (response.Result.IsSuccessStatusCode)
                {
                    result = response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return null;
                }
            }
           var reponseModel = JsonConvert.DeserializeObject<FacebookApiResponeVideo>(result);
            return reponseModel.source;
        }
        public bool CheckLink(string Link)
        {
           
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(Link);
            request.Method = "HEAD";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch 
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }
    }
}
