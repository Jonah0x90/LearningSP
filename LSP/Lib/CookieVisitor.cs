using System;
using System.Collections.Generic;
using CefSharp;

namespace LSP.Lib
{
    public class CookieVisitor : ICookieVisitor
    {
        public event Action<CefSharp.Cookie> SendCookie;

        public void Dispose()
        {

        }

        public CookieVisitor()
        {
            IsReady = true;
        }

        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            lock (this)
            {
                if (AllCookies.ContainsKey(cookie.Name))
                {
                    AllCookies[cookie.Name] = new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
                    AllCookies[cookie.Name].Name = cookie.Name;
                    AllCookies[cookie.Name].Value = cookie.Value;
                    AllCookies[cookie.Name].Path = cookie.Path;
                    AllCookies[cookie.Name].Domain = cookie.Domain;
                }
                else
                    AllCookies.Add(cookie.Name, new System.Net.Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
                //fire when complete
                IsReady = count == total - 1;

                //deleteCookie = false;
                //SendCookie?.Invoke(cookie);
                //return true
            }
            return true;
        }

        public Dictionary<string, System.Net.Cookie> AllCookies { get; } = new Dictionary<string, System.Net.Cookie>();
        public bool IsReady { get; set; }
        public System.Net.Cookie this[string name]
        {
            get { return AllCookies.ContainsKey(name) ? AllCookies[name] : null; }
        }

        ~CookieVisitor()
        {

        }
    }
}
