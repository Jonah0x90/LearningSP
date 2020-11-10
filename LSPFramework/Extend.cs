using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSPFramework
{
    public class Extend
    {
        public static Extend Instance { get; } = new Extend();

        public string GetTimeStamp(bool flag = true)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            if (flag)
                return Convert.ToInt64(ts.TotalSeconds).ToString();
            else
                return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        public string GetUrlParam(string name,string url)
        {
            url = System.Web.HttpUtility.UrlDecode(url);
            // 如果链接没有参数，或者链接中不存在我们要获取的参数，直接返回空
            if (url.IndexOf("?") == -1 || url.IndexOf(name + '=') == -1)
            {
                return string.Empty;
            }
            // 获取链接中参数部分
            var queryString = url.Substring(url.IndexOf("?") + 1);
            if (queryString.IndexOf('#') > -1)
            {
                queryString = queryString.Substring(0, queryString.IndexOf('#'));
            };
            // 分离参数对 ?key=value&key2=value2
            var parameters = queryString.Split('&');
            var pos = 0;
            var paraName = string.Empty;
            var paraValue = string.Empty;
            for (var i = 0; i < parameters.Length; i++)
            {
                // 获取等号位置
                pos = parameters[i].IndexOf('=');
                if (pos == -1)
                {
                    continue;
                }
                // 获取name 和 value
                paraName = parameters[i].Substring(0, pos);
                paraValue = parameters[i].Substring(pos + 1);
                // 如果查询的name等于当前name，就返回当前值，同时，将链接中的+号还原成空格
                if (paraName == name)
                {
                    return System.Web.HttpUtility.UrlDecode(paraValue.Replace("+", " "));
                }
            }
            return string.Empty;
        }


        public string CreateQR(string text)
        {
            QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.M);
            QRCode qrcode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 15, 6, false);
            //MemoryStream ms = new MemoryStream();
            //qrCodeImage.Save(ms, ImageFormat.Jpeg);
            qrCodeImage.Save($"{AppDomain.CurrentDomain.BaseDirectory}{RF.BarCodeName}.jpg");
            return $"{AppDomain.CurrentDomain.BaseDirectory}{RF.BarCodeName}.jpg";
            // 如果想保存图片 可使用  qrCodeImage.Save(filePath);

        }


    }
}
