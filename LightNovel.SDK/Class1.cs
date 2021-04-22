using Synctool.HttpFramework;
using Synctool.LinqFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace LightNovel.SDK
{
    public class Class1
    {
        //https://github.com/0x7FFFFF/wenku8downloader/blob/master/src/user.py
        private const string Host = "https://www.wenku8.net/login.php";

        public Class1()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public void T()
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.Add(new KeyValuePair<string, string>("username", "kilydoll365"));
            pairs.Add(new KeyValuePair<string, string>("password", "sion8550"));
            pairs.Add(new KeyValuePair<string, string>("usercookie", "0"));
            pairs.Add(new KeyValuePair<string, string>("action", "login"));
            pairs.Add(new KeyValuePair<string, string>("submit", "%26%23160%3B%B5%C7%26%23160%3B%26%23160%3B%C2%BC%26%23160%3B"));



            var bytes = HttpMultiClient.HttpMulti.InitCookieContainer().AddNode(Host, pairs, RequestType.POST)
                .Build().RunBytes(cookie =>
                {
                    var cok = cookie.GetCookies(new Uri(Host)).Cast<Cookie>().ToList();
                }).FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            var xx = reader.ReadToEnd();
            Console.WriteLine(xx);
        }
    }
}
