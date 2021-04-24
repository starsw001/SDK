using HtmlAgilityPack;
using LightNovel.SDK.ViewModel;
using Synctool.CacheFramework;
using Synctool.HttpFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK
{
    internal class LightNovel : ILightNovel
    {
        //https://github.com/0x7FFFFF/wenku8downloader/blob/master/src/user.py
        private const string Host = "https://www.wenku8.net";
        private const string Login = Host + "/login.php";

        public LightNovelResponseOutput LightNovelInit(LightNovelRequestInput Input)
        {
            var response = HttpMultiClient.HttpMulti.InitCookieContainer()
                  .AddNode(Login, Input.InitParam, Input.InitParam.FieldMap, RequestType.POST, "GBK")
                  .AddNode(Host, RequestType.GET, "GBK")
                 .Build().RunString((Cookie, Uri) =>
                 {
                     if (Caches.RunTimeCacheGet<CookieCollection>(Host) == null)
                         Caches.RunTimeCacheSet(Host, Cookie.GetCookies(Uri), 1440);
                 }).LastOrDefault();


            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            return default;
        }
    }
}
