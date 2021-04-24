using HtmlAgilityPack;
using LightNovel.SDK.ViewModel;
using LightNovel.SDK.ViewModel.Response;
using Synctool.CacheFramework;
using Synctool.HttpFramework;
using Synctool.LinqFramework;
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
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                CategoryResults = new List<LightNovelCategoryResult>()
            };

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

            List<string> FilterCategory = new List<string>
            {
             "轻小说列表","热门轻小说","动画化作品","今日更新","新书一览","完结全本"
            };
            document.DocumentNode.SelectNodes("//ul[@class='navlist']/li/a").ForEnumerEach(node =>
            {
                if (FilterCategory.Contains(node.InnerText))
                {
                    LightNovelCategoryResult category = new LightNovelCategoryResult
                    {
                        CategoryAddress = node.GetAttributeValue("href", ""),
                        CategoryName = node.InnerText
                    };
                    Result.CategoryResults.Add(category);
                }
            });

            return Result;
        }
    }
}
