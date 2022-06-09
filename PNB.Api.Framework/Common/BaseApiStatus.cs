using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Api.Framework.Common
{
    public enum BaseApiStatus
    {
        NotFound = -3,
        ErrorSystem = -2,
        Authorization401 = -1,
        Success = 1,
        Failed = 0,
    }
}
