using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using log4net;
using Newtonsoft.Json;

namespace LSPWebAPI.APP_Context
{
    public class AuthFilter : ActionFilterAttribute
    {
        private log4net.ILog log = log4net.LogManager.GetLogger("AuthFilter");
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            log.Info(JsonConvert.SerializeObject(new
            {
                PortName = actionExecutedContext.Request.RequestUri.AbsolutePath,
                RequestType = actionExecutedContext.Request.Method.ToString(),
                StatusCode = Convert.ToInt32(new HttpResponseMessage(HttpStatusCode.OK).StatusCode),//设置状态码
                ClientIp = GetClientIp(),
                ParameterList = actionExecutedContext.ActionContext.ActionArguments,//获得参数值
                Success = true
            }));
        }
        /// <summary>
        /// 获取客户端Ip
        /// </summary>
        /// <returns></returns>
        private string GetClientIp()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                return "0.0.0.0";
            }
            return result;
        }
    }
}