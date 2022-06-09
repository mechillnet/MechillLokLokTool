using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PNB.Service.sAuthenticationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sNotification
{
   public partial class NotificationService : INotificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
     
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private readonly IWorkContext _workContext;
        public NotificationService(IHttpContextAccessor httpContextAccessor,
           ITempDataDictionaryFactory tempDataDictionaryFactory,
           IWorkContext workContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _workContext = workContext;
        }
        protected virtual void PrepareTempData(string key,string message)
        {
            var context = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(context);
            tempData[key] = message;
        }
        public virtual void SuccessNotification(string message)
        {
            PrepareTempData("Success", message);
        }
        public virtual void ErrorNotification(string message)
        {
            PrepareTempData("Error", message);
        }
        public virtual void WarningNotification(string message)
        {
            PrepareTempData("Warning", message);
        }

    }
}
