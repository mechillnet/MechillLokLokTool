using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sNotification
{
    public interface INotificationService
    {
        void SuccessNotification(string message);
        void ErrorNotification(string message);

        void WarningNotification(string message);
    }
}
