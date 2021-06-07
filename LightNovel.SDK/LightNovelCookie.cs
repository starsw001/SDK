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
            IHttpMultiClient.HttpMulti.InitCookie()
                .InitWebProxy((Proxy ?? new LightNovelProxy()).ToMapper<MultiProxy>())
                .AddNode(opt=> {
                    opt.NodePath = Login;
                    opt.ReqType = MultiType.POST;
                    opt.Encoding = "GBK";
                    opt.EntityParam = Input;
                    opt.MapFied = Input.FieldMap;
                }).Build().RunString((Cookie, Uri) =>
                {
                    if (Caches.RunTimeCacheGet<CookieCollection>(Host) == null)
                        Caches.RunTimeCacheSet(Host, Cookie.GetCookies(Uri), 1440);
                });
        }
    }
}
