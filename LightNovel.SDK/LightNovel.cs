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
using System.Web;

namespace LightNovel.SDK
{
    internal class LightNovel : ILightNovel
    {
        //https://github.com/0x7FFFFF/wenku8downloader/blob/master/src/user.py
        //https://github.com/chiro2001/Wenku8ToEpub-Online/blob/master/wenku8toepub.py
        private const string Host = "https://www.wenku8.net";
        private const string Login = Host + "/login.php";
        private const string Search = Host + "/modules/article/search.php?searchtype={0}&searchkey={1}";

        public LightNovelResponseOutput LightNovelInit(LightNovelRequestInput Input)
        {
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                CategoryResults = new List<LightNovelCategoryResult>()
            };

            var response = HttpMultiClient.HttpMulti.InitCookieContainer()
                  .AddNode(Login, Input.Init, Input.Init.FieldMap, RequestType.POST, "GBK")
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

        public LightNovelResponseOutput LightNovelSearch(LightNovelRequestInput Input)
        {
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                CategoryResults = new List<LightNovelCategoryResult>()
            };
            var SearchType = Input.Search.SearchType.ToString().ToLower();
            var KeyWord = HttpUtility.UrlEncode(Input.Search.KeyWord, Encoding.GetEncoding("GBK"));
            var response = HttpMultiClient.HttpMulti.InitCookieContainer()
                .Cookie(new Uri(Search), Caches.RunTimeCacheGet<CookieCollection>(Host))
                .AddNode(string.Format(Search, SearchType, KeyWord), RequestType.GET, "GBK")
                .Build().RunString().FirstOrDefault();
            
            var xx = response;
            throw new NotImplementedException();
        }

        public LightNovelResponseOutput LightNovelCategory(LightNovelRequestInput Input)
        {
            throw new NotImplementedException();
        }

        public LightNovelResponseOutput LightNovelDetail(LightNovelRequestInput Input)
        {
            throw new NotImplementedException();
        }

        public LightNovelResponseOutput LightNovelView(LightNovelRequestInput Input)
        {
            throw new NotImplementedException();
        }
    }
}
