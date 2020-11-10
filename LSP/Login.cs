using LSPFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LSP
{
    public partial class Login : Form
    {
        private IList<byte> VerifySrc { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var tokon = string.Empty;
            var generate = JObject.Parse(RF.Instance.Get(RF.QRgenerate, Encoding.UTF8));
            if (generate.Value<bool>("success") == true)
                tokon = generate.Value<string>("result");
            //var text = $"https://oapi.dingtalk.com/connect/qrcommit?showmenu=false&code={tokon}&appid={Extend.Instance.GetUrlParam("appid", RF.PreText)}&redirect_uri={Extend.Instance.GetUrlParam("redirect_uri", RF.PreText)}";
            var text = $"https://oapi.dingtalk.com/connect/qrcommit?showmenu=false&code={tokon}&appid=dingoankubyrfkttorhpou&redirect_uri=https://pc-api.xuexi.cn/open/api/sns/callback";
            var barCode = Extend.Instance.CreateQR(text);
            Thread.Sleep(50);
            PicBox_Verify.Image = Image.FromFile(barCode);
            PicBox_Verify.Height = PicBox_Verify.Image.Height;
            PicBox_Verify.Width = PicBox_Verify.Image.Width;
        }
    }
}
