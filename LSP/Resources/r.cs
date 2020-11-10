using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Resources
{
    public static class r
    {
        public const string l = "https://oapi.dingtalk.com/connect/qrconnect?appid=dingoankubyrfkttorhpou&response_type=code&scope=snsapi_login&redirect_uri=https://pc-api.xuexi.cn/open/api/sns/callback";
        public const string nn = "https://www.xuexi.cn/lgdata/u1ght1omn2.json?_st=25923677";
        public const string n = "https://www.xuexi.cn/98d5ae483720f701144e4dabf99a4a34/data5957f69bffab66811b99940516ec8784.js";
        public const string vv = "https://www.xuexi.cn/8e35a343fca20ee32c79d67e35dfca90/data7f9f27c65e84e71e1b7189b7132b4710.js";
        public const string v = "https://www.xuexi.cn/4426aa87b0b64ac671c96379a3a8bd26/datadb086044562a57b441c24f2af1c8e101.js";
        public const string g = "https://pc-api.xuexi.cn/open/api/score/get";
        public const string t = "https://pc-api.xuexi.cn/open/api/score/today/query";
        public const string q = "https://pc-api.xuexi.cn/open/api/score/today/queryrate";
        public const string wp = "<RSAKeyValue><Modulus>1sa7PrEiVW80ggiffDDOawIb7b3KhMD1AueqKhYLgYHRydJ7rmvEn3rtDC8PKuchvrRsT5V6JjbUefbZbo3iP+5iFpjEXB1sfOy2Z8mfhehhR2AH0UF+H48cwh+u9uSfJL/K0hzFtFrRpJ6Z1rgwlNUPUVyA5vRTf1i4cf7JRK8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public const string fp = "<RSAKeyValue><Modulus>oEgGmFjqFS6vL6vrYvu6Q2kSd3fimLRz3JE2QVM/SGW7V9XfH1RjPoEJA9tDWfZAMasDBqrSRcd5d/FC4USPJRY7oDwYCJH07Jcgf2gS+6vdWPciNx/W7hiX2HbzU/D3t6h9aAj+SKSR8ve3kMQTxq4QdriAgEhjUzm+pwAQH30=</Modulus><Exponent>AQAB</Exponent><P>zMfHAaHNeuw52+ZUbht4kD3KXqvEhbvN4k6ZVPl1GB5KvxtbxnhFcP/4dvlbKI20WsNFiOzAkACpLQcgAAVsWQ==</P><Q>yF7yOvCsyf8G0e8iHwOKxqZvfCbaA+x7ijf7EULaXmDl/qGSb0crQMGvcJlYqEt79XjktVNehEfgcfA0LG3XxQ==</Q><DP>w0ZymDTSPDjNh8uhkYnysyGpPBPyCFEpqHepMeXb6k7gmlKddqo0FeiR5+orKoXOiYqSYVra2bc3nC+iLUL+qQ==</DP><DQ>TbTErCb6W7wToQbkbLKq9y9EvRk0I6Fqp8feDmum1EMv7vgqGg23sH1s1HYj8+CSSjiAOfIDDbnyST2mfjR7IQ==</DQ><InverseQ>Yu2Et4pA1isn1DDM+sI4vbmdQ3MRulriAr4t0ebIYreiQev4VE/J6a8XQ3cHj4rKe0hl29OKXZrBhwhfvX3A8g==</InverseQ><D>I6iyku1hMtI+loAMr+pqP7oeKicpaijniABkjjcLortDaWDDMbCwHcVOcKW0/8xJ7uLpmu+hNXYSH6203/JFWDRNJrFdkNGx36XZFEDGO48IF7RQPxXWjbtcYgTH+v6WVwneRZxKZHf4gGK4mNGiTLbWVydxsE6L0SOWyeYbhTE=</D></RSAKeyValue>";
        public const string i = "http://xxapi.iuu.me:60991/api/v1.0/lsp/initialize?data=";
        public const string h = "http://xxapi.iuu.me:60991/api/v1.0/lsp/heartbeat";
        public static bool a = false;
        

        public static string f(string s)
        {
            s = s.Remove(0, s.IndexOf("{") + 1);
            s = s.Insert(0, "{");
            s = s.Remove(s.LastIndexOf(";"), 1);
            return s;
        }
        
    }
}
