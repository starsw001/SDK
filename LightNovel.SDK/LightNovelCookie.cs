using LightNovel.SDK.ViewModel.Request;
using Synctool.CacheFramework;
using Synctool.HttpFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK
{
    internal class LightNovelCookie : ILightNovelCookie
    {
        private const string Host = "https://www.wenku8.net";
        private const string Login = Host + "/login.php";
        public void RefreshCookie(LightNovelRefresh Input)
        {
            HttpMultiClient.HttpMulti.InitCookieContainer()
                 .AddNode(Login, Input, Input.FieldMap, RequestType.POST, "GBK")
                 .Build().RunString((Cookie, Uri) =>
                 {
                     if (Caches.RunTimeCacheGet<CookieCollection>(Host) == null)
                         Caches.RunTimeCacheSet(Host, Cookie.GetCookies(Uri), 1440);
                 });
        }
    }
}
