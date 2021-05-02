using LightNovel.SDK.ViewModel.Request;
using XExten.Advance.CacheFramework;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using System.Net;

namespace LightNovel.SDK
{
    internal class LightNovelCookie : ILightNovelCookie
    {
        private const string Host = "https://www.wenku8.net";
        private const string Login = Host + "/login.php";
        public void RefreshCookie(LightNovelRefresh Input, LightNovelProxy Proxy)
        {
            IHttpMultiClient.HttpMulti.InitCookieContainer()
                .InitWebProxy(Proxy.ToMapper<ProxyURL>())
                .AddNode(Login, Input, Input.FieldMap, RequestType.POST, "GBK")
                .Build().RunString((Cookie, Uri) =>
                {
                    if (Caches.RunTimeCacheGet<CookieCollection>(Host) == null)
                        Caches.RunTimeCacheSet(Host, Cookie.GetCookies(Uri), 1440);
                });
        }
    }
}
