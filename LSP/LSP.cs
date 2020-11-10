using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using LSP.Lib;
using LSP.Resources;
using LSPFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LSP
{
    public partial class LSP : Form
    {

        private ChromiumWebBrowser chromium;
        private CookieVisitor cookieVisitor;
        private delegate void MsgChanger(string msg);
        private delegate void StatusChanger(string status);
        private delegate void l();
        private List<New> n = new List<New>();
        private List<Video> v = new List<Video>();
        private bool ol;
        private int lt;
        private Thread t;
        private Thread d;
        private Thread f;
        private List<bool> o;

        private string Cookies { get; set; }

        public LSP()
        {
            InitializeComponent();
        }

        private void LSP_Load(object sender, EventArgs e)
        {
            o = new List<bool>();
            InitializeChromium();
            InitializeNew();
            InitializeVideo();
            t = new Thread(new ThreadStart(PointStatus));
            t.Start();
            d = new Thread(new ThreadStart(LeaningPlan));
            d.Start();
            f = new Thread(new ThreadStart(Rock));
            f.Start();
        }

        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings
            {
                PersistSessionCookies = true
            };
            Cef.Initialize(settings);

            //https://media.w3.org/2010/05/sintel/trailer.mp4
            chromium = new ChromiumWebBrowser(r.l)
            {
                MenuHandler = new MenuHandler(),
                Dock = DockStyle.Fill            
            };
            chromium.FrameLoadEnd += Browser_FrameLoadEnd;
            cookieVisitor = new CookieVisitor();

            panel_Web.Controls.Add(chromium);

        }

        private void InitializeNew()
        {
            var t = (JArray)JsonConvert.DeserializeObject(RF.Instance.Get(r.nn, Encoding.UTF8, RF.Instance.CreateCC(cookieVisitor.AllCookies)));
            if (Cache.GetCache("nc") == null)
            {
                Cache.SetCache("nc", t.Count);
                Cache.SetCache("ncl", t);
            }
            else if ((int)Cache.GetCache("nc") != t.Count)
            {
                Cache.SetCache("nc", t.Values().Count());
                Cache.SetCache("ncl", t);
            }
            var c = (JArray)Cache.GetCache("ncl");
            foreach (var item in c)
            {
                n.Add(new New
                {
                    Id = item.Value<string>("itemId"),
                    Url = item.Value<string>("url"),
                    Date = item.Value<string>("publishTime")
                });
            }
        }

        private void InitializeVideo()
        {
            var t = JObject.Parse((r.f(RF.Instance.Get(r.v, Encoding.UTF8, RF.Instance.CreateCC(cookieVisitor.AllCookies)))));
            if (Cache.GetCache("vc") == null)
            {
                Cache.SetCache("vc", (int)t.Values().First()["count"]);
                Cache.SetCache("vcl", t);
            }
            else if ((int)Cache.GetCache("nc") != (int)t.Values().First()["count"])
            {
                Cache.SetCache("vc", t.Values().First()["count"]);
                Cache.SetCache("vcl", t);
            }
            var c = (JObject)Cache.GetCache("vcl");
            foreach (var item in c["fp2t40yxso9xk0010"]["list1"])
            {
                v.Add(new Video
                {
                    Id = Convert.ToString(item["_id"]),
                    Url = item["static_page_url"].ToString(),
                    FirstName = item["frst_name"].ToString()
                });
            }
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                e
                    .Browser
                    .MainFrame
                    .ExecuteJavaScriptAsync(
                    "document.body.style.overflow = 'hidden'");
            }
            Cef.GetGlobalCookieManager().VisitAllCookies(cookieVisitor);
            Cef.GetGlobalCookieManager().VisitUrlCookies(e.Url, true, cookieVisitor);
        }

        private void LSP_FormClosing(object sender, FormClosingEventArgs e)
        {
            ThreadContorl(Threads.Learn, TStatus.Stop);
            ThreadContorl(Threads.Point, TStatus.Stop);
            ThreadContorl(Threads.Rock, TStatus.Stop);
            //Application.Exit();
            //Environment.Exit(Environment.ExitCode);
            Process.GetCurrentProcess().Kill();
        }

        private void PointStatus()
        {
            while (true)
            {
                if (cookieVisitor.AllCookies.Count > 0)
                {
                    if (!cookieVisitor.AllCookies.ContainsKey("token"))
                    {
                        Msg("请扫描二维码登录");
                    }
                    else
                    {
                        var t = JObject.Parse(RF.Instance.Get(r.g, Encoding.UTF8, RF.Instance.CreateCC(cookieVisitor.AllCookies)));
                        var c = JObject.Parse(RF.Instance.Get(r.t, Encoding.UTF8, RF.Instance.CreateCC(cookieVisitor.AllCookies)));
                        var i = GetPoint();
                        if (t.Value<int>("code") == 200 && t.Value<bool>("ok") && c.Value<int>("code") == 200 && c.Value<bool>("ok") && i.Value<int>("code") == 200 && i.Value<bool>("ok"))
                        {
                            var pr = System.Web.HttpUtility.UrlEncode(RSACryption.Instance.RSAEncrypt(r.wp, t["data"]["userId"].ToString()));
                            var b = RF.Instance.Get($"{r.i}{pr}", Encoding.UTF8);
                            var g = JObject.Parse(b);
                            var n = RSACryption.Instance.RSADecrypt(r.fp, g["access_key"].ToString());
                            if (n.Substring(0, 4) != "T+CM")
                            {
                                Msg($"登录成功. 用户ID:{t["data"]["userId"]}  学习积分:{t["data"]["score"].ToString()}  今日积分:{c["data"]["score"].ToString()}");
                                if (o.Count == 4 && o.All(v => v == true))
                                {
                                    Status($"今日学习已结束");
                                    ThreadContorl(Threads.Point, TStatus.Stop);
                                    ThreadContorl(Threads.Learn, TStatus.Stop);
                                    ThreadContorl(Threads.Rock, TStatus.Stop);
                                }
                                switch (lt)
                                {
                                    case 0:
                                        var m = i["data"]["dayScoreDtos"].Where(v => (int)v["ruleId"] == 1).ToList().First();
                                        if ((int)m["currentScore"] != (int)m["dayMaxScore"])
                                        {
                                            Status($"({(int)m["currentScore"]}/{(int)m["dayMaxScore"]})正在学习文章...");
                                        }
                                        else
                                        {
                                            o.Add(true);
                                            Status($"({(int)m["currentScore"]}/{(int)m["dayMaxScore"]})学习文章结束...");
                                            ThreadContorl(Threads.Learn, TStatus.Restart);
                                        }
                                        break;
                                    case 1:
                                        var q = i["data"]["dayScoreDtos"].Where(v => (int)v["ruleId"] == 2).ToList().First();
                                        if ((int)q["currentScore"] != (int)q["dayMaxScore"])
                                        {
                                            Status($"({(int)q["currentScore"]}/{(int)q["dayMaxScore"]})正在学习视频...");
                                        }
                                        else
                                        {
                                            o.Add(true);
                                            Status($"({(int)q["currentScore"]}/{(int)q["dayMaxScore"]})学习视频结束...");
                                            ThreadContorl(Threads.Learn, TStatus.Restart);
                                        }
                                        break;
                                    case 2:
                                        var j = i["data"]["dayScoreDtos"].Where(v => (int)v["ruleId"] == 1002).ToList().First();
                                        if ((int)j["currentScore"] != (int)j["dayMaxScore"])
                                        {
                                            Status($"({(int)j["currentScore"]}/{(int)j["dayMaxScore"]})正在思考文章...");
                                        }
                                        else
                                        {
                                            o.Add(true);
                                            Status($"({(int)j["currentScore"]}/{(int)j["dayMaxScore"]})思考文章结束...");
                                            ThreadContorl(Threads.Learn, TStatus.Restart);
                                        }
                                        break;
                                    case 3:
                                        var k = i["data"]["dayScoreDtos"].Where(v => (int)v["ruleId"] == 1003).ToList().First();
                                        if ((int)k["currentScore"] != (int)k["dayMaxScore"])
                                        {
                                            Status($"({(int)k["currentScore"]}/{(int)k["dayMaxScore"]})正在思考视频...");
                                        }
                                        else
                                        {
                                            o.Add(true);
                                            Status($"({(int)k["currentScore"]}/{(int)k["dayMaxScore"]})思考视频结束...");
                                            ThreadContorl(Threads.Learn, TStatus.Restart);
                                        }
                                        break;
                                }
                                ol = true;
                                l s;
                                s = new l(() => { panel_Web.Enabled = false; });
                                panel_Web.Invoke(s);
                                try
                                {
                                    var rs = RF.Instance.Get(r.h, pr, System.Web.HttpUtility.UrlEncode(RSACryption.Instance.RSAEncrypt(r.wp, n.ToString())), Encoding.UTF8);
                                    var e = new Evaluator("t", "m", RSACryption.Instance.RSADecrypt(r.fp, JObject.Parse(rs)["beats"].ToString()));
                                    r.a = true;
                                    //e.RunMethhod("s");
                                }
                                catch(Exception ex)
                                {
                                    l vs;
                                    vs = new l(() => { panel_Web.Visible = false; });
                                    panel_Web.Invoke(s);
                                    Msg("心跳异常服务已停止");
                                    Status("发呆中...Zzzzzzzzzzzz");
                                    ThreadContorl(Threads.Point, TStatus.Stop);
                                    ThreadContorl(Threads.Learn, TStatus.Stop);
                                    ThreadContorl(Threads.Rock, TStatus.Stop);
                                }
                            }
                            else
                            {
                                l s;
                                s = new l(() => { panel_Web.Visible = false; });
                                panel_Web.Invoke(s);
                                Msg($"登录成功. 抱歉!暂时无法为用户ID[{t["data"]["userId"]}]提供服务");
                                ThreadContorl(Threads.Point, TStatus.Stop);
                                ThreadContorl(Threads.Learn, TStatus.Stop);
                                ThreadContorl(Threads.Rock, TStatus.Stop);
                            }
                        }
                    }
                }
                if (!ol)
                    Thread.Sleep(1000);
                else
                    Thread.Sleep(new Random().Next(5000, 15000));
            }
        }

        private void LeaningPlan()
        {
            while(true)
            {
                while (ol && r.a)
                {
                    var t = GetPoint();
                    if (t.Value<int>("code") == 200 && t.Value<bool>("ok"))
                    {
                        foreach (var i in t["data"]["dayScoreDtos"])
                        {
                            switch ((int)(i["ruleId"]))
                            {
                                case 1://阅读文章
                                    if ((int)i["currentScore"] < (int)i["dayMaxScore"])
                                    {
                                        lt = 0;
                                        Leaning(0, new Random().Next(15000, 60000));
                                    }
                                    break;
                                case 2://观看视频
                                    if ((int)i["currentScore"] < (int)i["dayMaxScore"])
                                    {
                                        lt = 1;
                                        Leaning(1, new Random().Next(15000, 60000));
                                    }
                                    break;
                                case 1002://文章时长
                                    if ((int)i["currentScore"] < (int)i["dayMaxScore"])
                                    {
                                        lt = 2;
                                        Leaning(0, 1000 * 60 * 33);
                                    }
                                    break;
                                case 1003://视频时长
                                    if ((int)i["currentScore"] < (int)i["dayMaxScore"])
                                    {
                                        lt = 3;
                                        Leaning(1, 1000 * 60 * 51);
                                    }
                                    break;
                                default:
                                    Status("发呆中...Zzzzzzzzzzzz");
                                    break;
                            }
                        }
                    }
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
            }
        }

        private void Leaning(int tpye, int time)
        {
            if (ActiveCheck())
                time /= 2;
            switch (tpye)
            {
                case 0:
                    n = n.Where(v => Convert.ToDateTime(v.Date) <= DateTime.Now && Convert.ToDateTime(v.Date) >= (DateTime.Now.AddDays(-7))).ToList();
                    chromium.Load(n.First().Url);
                    n.RemoveAt(0);
                    Thread.Sleep(time);
                    break;
                case 1:
                    chromium.Load(v.First().Url);
                    v.RemoveAt(0);
                    Thread.Sleep(time);
                    break;
                default:
                    break;
            }
        }

        private bool ActiveCheck()
        {
            var h = DateTime.Now.TimeOfDay;
            if (h >= DateTime.Parse("06:00").TimeOfDay && h <= DateTime.Parse("08:30").TimeOfDay)
            {
                return true;
            }
            else if (h >= DateTime.Parse("12:00").TimeOfDay && h <= DateTime.Parse("14:00").TimeOfDay)
            {
                return true;
            }
            else if (h >= DateTime.Parse("20:00").TimeOfDay && h <= DateTime.Parse("22:30").TimeOfDay)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private JObject GetPoint()
        {
            return JObject.Parse(RF.Instance.Get(r.q, Encoding.UTF8, RF.Instance.CreateCC(cookieVisitor.AllCookies)));
        }

        private void Rock()
        {
            while (true)
            {
                while (ol)
                {
                    //chromium.ExecuteScriptAsync(@"
                    //    var height = document.body.clientHeight;
                    //    var number = 0;
                    //    var length = 0;
                    //    var frequency = 10000;
                    //    var time = setInterval(function(){
                    //        number += 1;
                    //        if(number == frequency + 1){
                    //            clearInterval(time);
                    //        } else {
                    //            length += height / frequency;
                    //            document.documentElement.scrollTop = length;
                    //        }})
                    //");
                    chromium.ExecuteScriptAsync(@"
                         var timer = setInterval(function(){
                        window.scrollBy(0, 100);
                            if (getScrollTop() + getWindowHeight() == getScrollHeight())
                            {
                                clearInterval(timer);
                            }
                        },1000);
                    ");
                    Thread.Sleep(30000);
                }
                Thread.Sleep(1000);
            }
        }

        private void ThreadContorl(Threads v, TStatus s)
        {
            switch (s)
            {
                case TStatus.Start:
                    switch (v)
                    {
                        case Threads.Point:
                            if (t.IsAlive)
                            {
                                t.Abort();
                            }
                            t = new Thread(new ThreadStart(PointStatus));
                            t.Start();
                            break;
                        case Threads.Learn:
                            if (d.IsAlive)
                            {
                                d.Abort();
                            }
                            d = new Thread(new ThreadStart(LeaningPlan));
                            d.Start();
                            break;
                        case Threads.Rock:
                            if (f.IsAlive)
                            {
                                f.Abort();
                            }
                            f = new Thread(new ThreadStart(Rock));
                            f.Start();
                            break;
                    }
                break;
                case TStatus.Stop:
                    switch (v)
                    {
                        case Threads.Point:
                            if (t != null && t.IsAlive)
                                t.Abort();
                            break;
                        case Threads.Learn:
                            if(d != null && d.IsAlive)
                                d.Abort();
                            break;
                        case Threads.Rock:
                            if (f != null && f.IsAlive)
                                f.Abort();
                            break;
                    }
                break;
                case TStatus.Restart:
                    switch(v)
                    {
                        case Threads.Point:
                            if (t.IsAlive)
                            {
                                t.Abort();
                            }
                            t = new Thread(new ThreadStart(PointStatus));
                            t.Start();
                            break;
                        case Threads.Learn:
                            if (d.IsAlive)
                            {
                                d.Abort();
                            }
                            d = new Thread(new ThreadStart(LeaningPlan));
                            d.Start();
                            break;
                        case Threads.Rock:
                            if (f.IsAlive)
                            {
                                f.Abort();
                            }
                            f = new Thread(new ThreadStart(Rock));
                            f.Start();
                            break;
                    }
                break;
            }
        }

        private void Msg(string text)
        {
            if (Lab_Msg.InvokeRequired)
            {
                var outdelegate = new MsgChanger(Msg);
                BeginInvoke(outdelegate, new object[] { text });
                return;
            }
            Lab_Msg.Text = text;
        }

        private void Status(string text)
        {
            if (Lab_Status.InvokeRequired)
            {
                var outdelegate = new StatusChanger(Status);
                BeginInvoke(outdelegate, new object[] { text });
                return;
            }
            Lab_Status.Text = text;
        }
    }

    public enum Threads
    {
        Point,
        Learn,
        Rock
    }

    public enum TStatus
    {
        Start,
        Stop,
        Restart
    }
}
