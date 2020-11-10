using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LSPFramework
{
    public class RF
    {
        public static RF Instance { get; } = new RF();

        public CookieContainer Cookie = new CookieContainer();

        public const string BarCodeName = "barcodetmp";

        public const string PreText = "https://login.dingtalk.com/login/qrcode.htm?goto=https%3A%2F%2Foapi.dingtalk.com%2Fconnect%2Fqrconnect%3Fappid%3Ddingoankubyrfkttorhpou%26response_type%3Dcode%26scope%3Dsnsapi_login%26redirect_uri%3Dhttps%3A%2F%2Fpc-api.xuexi.cn%2Fopen%2Fapi%2Fsns%2Fcallback&style=border%3Anone%3Bbackground-color%3A%23FFFFFF%3B";

        public const string QRgenerate = "https://login.dingtalk.com/user/qrcode/generate";

        public static bool a = false;


        public string Get(string url, Encoding encoding)
        {
            var uri = new Uri(url);
            var myRequest = (HttpWebRequest)WebRequest.Create(uri);
            myRequest.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myRequest.Accept = "*/*";
            myRequest.KeepAlive = true;
            myRequest.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            var result = (HttpWebResponse)myRequest.GetResponse();
            var receviceStream = result.GetResponseStream();
            var readerOfStream = new StreamReader(receviceStream, encoding);
            var strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        public string Get(string url, string access_key , string sign, Encoding encoding)
        {
            var uri = new Uri(url);
            var myRequest = (HttpWebRequest)WebRequest.Create(uri);
            myRequest.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myRequest.Accept = "*/*";
            myRequest.KeepAlive = true;
            myRequest.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            myRequest.Headers.Add("Authorization", $"LSP {access_key}:{sign}");
            var result = (HttpWebResponse)myRequest.GetResponse();
            var receviceStream = result.GetResponseStream();
            var readerOfStream = new StreamReader(receviceStream, encoding);
            var strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        public string Get(string url, Encoding encoding, CookieContainer cookie)
        {
            var uri = new Uri(url);
            var myRequest = (HttpWebRequest)WebRequest.Create(uri);
            myRequest.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myRequest.Accept = "*/*";
            myRequest.KeepAlive = true;
            myRequest.CookieContainer = cookie;
            myRequest.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            var result = (HttpWebResponse)myRequest.GetResponse();
            var receviceStream = result.GetResponseStream();
            var readerOfStream = new StreamReader(receviceStream, encoding);
            var strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        public string Get(string url, string referer, Encoding encoding, CookieContainer cookie)
        {
            var uri = new Uri(url);
            var myRequest = (HttpWebRequest)WebRequest.Create(uri);
            myRequest.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myRequest.Accept = "*/*";
            myRequest.KeepAlive = true;
            myRequest.CookieContainer = cookie;
            myRequest.Referer = referer;
            myRequest.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            var result = (HttpWebResponse)myRequest.GetResponse();
            var receviceStream = result.GetResponseStream();
            var readerOfStream = new StreamReader(receviceStream, encoding);
            var strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        public string Post(string url, string postdata, string contentType, string referer, ref CookieContainer cookie)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            request.Referer = referer;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
            request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
            request.KeepAlive = true;
            request.ContentType = contentType;
            request.Method = "POST";
            var encoding = Encoding.Default;
            byte[] postData = encoding.GetBytes(postdata);  
            request.ContentLength = postData.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);

            var response = (HttpWebResponse)request.GetResponse();
            cookie.Add(response.Cookies);
            var responseStream = response.GetResponseStream();
            if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }
            var streamReader = new StreamReader(responseStream, encoding);
            string retString = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            return retString;
        }

        public string Post(string url, string postdata, string contentType, string referer, CookieContainer Cookie)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = Cookie; 
            request.Referer = referer;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
            request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
            request.KeepAlive = true;
            request.ContentType = contentType;
            request.Method = "POST";
            var encoding = Encoding.Default; 
            byte[] postData = encoding.GetBytes(postdata);
            request.ContentLength = postData.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);
            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }
            var streamReader = new StreamReader(responseStream, encoding);
            string retString = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            return retString;
        }

        public IList<byte> GetVerifyPic(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.CookieContainer = new CookieContainer { };
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            List<byte> bytes = new List<byte>();
            while (true)
            {
                int bt = receviceStream.ReadByte();
                if (bt == -1)
                    break;
                bytes.Add((byte)bt);
            }
            receviceStream.Close();
            receviceStream.Dispose();
            result.Close();
            return bytes;
        }

        public IList<byte> GetVerifyPic(string url, string referer, CookieContainer cookie, ref CookieContainer cookies)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.CookieContainer = cookie;
            myReq.Referer = referer;
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            cookies.Add(result.Cookies);
            Stream receviceStream = result.GetResponseStream();
            List<byte> bytes = new List<byte>();
            while (true)
            {
                int bt = receviceStream.ReadByte();
                if (bt == -1)
                    break;
                bytes.Add((byte)bt);
            }
            receviceStream.Close();
            receviceStream.Dispose();
            result.Close();
            return bytes;
        }

        public CookieContainer CreateCC(Dictionary<string, System.Net.Cookie> dict)
        {
            var cc = new CookieContainer();
            if (dict.Count > 0)
            {
                foreach (var item in dict.Values)
                {
                    var cookie = new Cookie(item.Name, item.Value, item.Path, item.Domain);
                    cc.Add(cookie);
                }
                return cc;
            }
            else
            {
                return cc;
            }
        }

    }


}
