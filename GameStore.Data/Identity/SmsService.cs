
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace GameStore.Data.Identity
{
    public class SmsService : ISmsService
    {
        private const string SmscLogin = "Volltages";
        private const string SmscPassword = "13Avtobusus";
        private const string SmscCharset = "utf-8";

        public Task SendAsync(IdentityMessage message)
        {
            return Task.Run(() => SendSms(message.Destination, message.Body));
        }

        private string[] SendSms(string phone, string message, int translit = 0, string time = "", int id = 0, int format = 0, string sender = "", string query = "")
        {
            string[] formats = { "flash=1", "push=1", "hlr=1", "bin=1", "bin=2", "ping=1", "mms=1", "mail=1", "call=1" };

            var m = SmscSendCmd(
                "send",
                "cost=3&phones=" + _urlencode(phone) + "&mes=" + _urlencode(message) + "&id=" + id + "&translit="
                + translit + (format > 0 ? "&" + formats[format - 1] : string.Empty)
                + (sender != string.Empty ? "&sender=" + _urlencode(sender) : string.Empty)
                + (time != string.Empty ? "&time=" + _urlencode(time) : string.Empty)
                + (query != string.Empty ? "&" + query : string.Empty));
            return m;
        }

        private string[] SmscSendCmd(string cmd, string arg)
        {
            arg = "login=" + _urlencode(SmscLogin) + "&psw=" + _urlencode(SmscPassword) + "&fmt=1&charset=" + SmscCharset + "&" + arg;

            var url = "http://smsc.ua/sys/" + cmd + ".php" + "?" + arg;

            string ret;
            var i = 0;

            do
            {
                if (i++ > 0)
                {
                    url = url.Replace("smsc.ua/", "www" + i + ".smsc.ua/");
                }

                var request = (HttpWebRequest)WebRequest.Create(url);

                try
                {
                    var response = (HttpWebResponse)request.GetResponse();

                    var sr = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException());
                    ret = sr.ReadToEnd();
                }
                catch (WebException)
                {
                    ret = string.Empty;
                }
            }
            while (ret == string.Empty && i < 5);

            if (ret == string.Empty)
            {
                ret = ",";
            }

            var delim = ',';

            if (cmd == "status")
            {
                string[] par = arg.Split('&');

                for (i = 0; i < par.Length; i++)
                {
                    var lr = par[i].Split("=".ToCharArray(), 2);

                    if (lr[0] == "id" && lr[1].IndexOf("%2c", StringComparison.Ordinal) > 0)
                    {
                        delim = '\n';
                    }
                }
            }

            return ret.Split(delim);
        }

        private string _urlencode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }
    }
}
