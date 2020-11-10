using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace LSPWebAPI.APP_Context
{
    public class ExceptionHandling : ExceptionFilterAttribute, IExceptionFilter
    {
        private log4net.ILog log = log4net.LogManager.GetLogger("ExceptionHandling");
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var code = new HttpResponseMessage(HttpStatusCode.InternalServerError).StatusCode;
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            string msg = JsonConvert.SerializeObject(new BaseResult() { success = false, message = actionExecutedContext.Exception.Message });//返回异常错误提示
                                                                                                                                              //写入错误日志相关实现
            log.Error(JsonConvert.SerializeObject(new
            {
                PortName = actionExecutedContext.Request.RequestUri.AbsolutePath,
                RequestType = actionExecutedContext.Request.Method.ToString(),
                StatusCode = Convert.ToInt32(code),
                ClientIp = GetClientIp(),
                ParameterList = actionExecutedContext.ActionContext.ActionArguments.ToString(),
                Success = false,
                ErrorMessage = msg
            }));
            //result
            actionExecutedContext.Response.Content = new StringContent(msg, Encoding.UTF8);
        }
        /// <summary>
        /// 获取调用接口者ip地址
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
    public class BaseResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { get; set; }
    }
}