using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LSPWebAPI.APP_Context
{
    public class AuthFilterOutside : AuthorizeAttribute
    {
        private const string APIPriKey = "<RSAKeyValue><Modulus>1sa7PrEiVW80ggiffDDOawIb7b3KhMD1AueqKhYLgYHRydJ7rmvEn3rtDC8PKuchvrRsT5V6JjbUefbZbo3iP+5iFpjEXB1sfOy2Z8mfhehhR2AH0UF+H48cwh+u9uSfJL/K0hzFtFrRpJ6Z1rgwlNUPUVyA5vRTf1i4cf7JRK8=</Modulus><Exponent>AQAB</Exponent><P>12YLrZ0+/M6M6nu6yRCHEDVbMXQBwk2dEjV9tBbZopwGQU+BEG4KAcqVI4vlNlpwdupa6XReJihVRLg5Q4W7ZQ==</P><Q>/0Kn8EkKZeAYQC6mC4VhMbG3fPgDuCc0uV7YvUvsVVXwn4eDuOenh1rlC3YO5RHvcYjLePoAIJjlDBMSzrrggw==</Q><DP>rG6UireG5Pq09EF4ld0VQnR0PHKRtepMA3eu2awxLWuZ1k6/E1gDystR+NLU+14LCicyABGYDRPcrtaLgPJdwQ==</DP><DQ>6/QNkQuzVOCFCi8UxemRIoKIfjg0F/IFxqRp7PFVkLxUJOL7W9ym+3OF7cY/lnexwl0U2MsfewJaF4M6C2arSQ==</DQ><InverseQ>s4VCHsBJCl0JjwupNW1DM6zDcvS/Vl68FpV8zxoDhBMwf3o/JfcypZZShSvyz3EebMaqGCt1fNFOfWQ43srwtg==</InverseQ><D>AGiIt80yA1DFApZHnCUJNdTfZR7rDdn1qheMqd9rqC36AYgGUJLHeriK6NU4eMMCiNB8M26IuLfgxLz+aG8zbFDMbm6+eCSljCdEF8mxefynpAZoBTj2QJD/NABoO3n66xwCAXSHwWM3dseXIoj2KokxbTd9srrTWcQ/fcbEWVk=</D></RSAKeyValue>";

        //重写基类的验证方式，加入我们自定义的Ticket验证 
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var content = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;
            HttpRequestBase request = content.Request;
            var access_key = request.Headers.Get("Authorization");
            if (!string.IsNullOrEmpty(access_key))
            {
                if (ValidateTicket(access_key))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                if (isAnonymous) base.OnAuthorization(actionContext);
                else HandleUnauthorizedRequest(actionContext);
            }
        }

        //校验sign
        private bool ValidateTicket(string key)
        {
            try
            {
                key = System.Web.HttpUtility.UrlDecode(key);
                key = key.Remove(0, 4);
                key = key.Trim();
                var k = key.Split(':').ToList();
                if (k.Count == 2)
                {
                    var u = Cache.GetCache(RSACryption.Instance.RSADecrypt(Keys.APIPriKey, k.First()));
                    if (u == null)
                        return false;
                    var t = RSACryption.Instance.RSADecrypt(Keys.APIPriKey, k[1]);
                    if (u.ToString() == t)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}