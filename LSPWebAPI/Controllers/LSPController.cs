using LSPWebAPI.APP_Context;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LSPWebAPI.Controllers
{
    [RoutePrefix("api/v1.0/lsp")]
    public class LSPController : ApiController
    {
        private readonly string Auth = ConfigurationManager.AppSettings["Auth"];

        [Route("initialize")]
        [HttpGet]
        public IHttpActionResult Init(string data)
        {
            try
            {
                var d = RSACryption.Instance.RSADecrypt(Keys.APIPriKey, data);
                var k = Auth.Split(',').ToList();
                if (k.Contains(d))
                {
                    var t = new Random().Next(10000, 999999).ToString().Substring(3);
                    var s = Guid.NewGuid().ToString();
                    var p = System.Text.Encoding.UTF8.GetBytes(d + t + s);
                    var b = new System.Security.Cryptography.SHA256Managed().ComputeHash(p);
                    var h = Convert.ToBase64String(b);
                    Cache.SetCache(d, h);
                    return Json(new { access_key = RSACryption.Instance.RSAEncrypt(Keys.LspPubKey, h) });
                }
                else
                {
                    var t = $"T+CM{Extend.GetTimeStamp().Substring(4).Insert(2, new Random().Next(23751).ToString())}";
                    return Json(new { access_key = RSACryption.Instance.RSAEncrypt(Keys.LspPubKey, t) });
                }
            }
            catch
            {
                return Json(new {});
            }
        }


        [Route("heartbeat")]
        [AuthFilterOutside]
        [HttpGet]
        public IHttpActionResult HeartBeat()
        {
            var f = $"/*{Extend.GetTimeStamp().Substring(4) + new Random().Next(987654)}*/";
            var e = $"/*{Guid.NewGuid().ToString()}{new Random().Next(88888).ToString().Substring(2)}*/";
            string c = "using System;using LSPFramework; namespace t{class m{" + f + " public void s(){RF.a = true;}" + e + " }}";
            return Json(new { beats = RSACryption.Instance.RSAEncrypt(Keys.LspPubKey, c) });
        }
    }
}