using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Web.Framework.Extensions
{
    public interface ICipherService
    {
        string EncryptString(string text);
        string DecryptString(string text);
    }
}
