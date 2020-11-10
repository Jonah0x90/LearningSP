using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSPWebAPI.APP_Context
{
    public class Extend
    {

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}