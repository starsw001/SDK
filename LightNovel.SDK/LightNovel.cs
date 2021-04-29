using HtmlAgilityPack;
using LightNovel.SDK.ViewModel;
using LightNovel.SDK.ViewModel.Response;
using Synctool.CacheFramework;
using Synctool.HttpFramework;
using Synctool.HttpFramework.MultiCommon;
using Synctool.HttpFramework.MultiFactory;
using Synctool.LinqFramework;
using Synctool.StaticFramework;
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
        private const string Host = "https://www.wenku8.net";
        private const string Search = Host + "/modules/article/search.php?searchtype={0}&searchkey={1}&page={2}";

        private CookieCollection GetCookies()
        {
            return Caches.RunTimeCacheGet<CookieCollection>(Host);
        }

        public LightNovelResponseOutput LightNovelInit(LightNovelRequestInput Input, Action<ILightNovelCookie> action)
        {
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                CategoryResults = new List<LightNovelCategoryResult>()
            };

            if (GetCookies() == null)
                action.Invoke(new LightNovelCookie());

            var response = IHttpMultiClient.HttpMulti.InitCookieContainer()
                .InitWebProxy(Input.Proxy.ToMapper<ProxyURL>())
                .Cookie(new Uri(Host), GetCookies())
                .AddNode(Host, RequestType.GET, "GBK")
                .Build().RunString().FirstOrDefault();

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

        public LightNovelResponseOutput LightNovelSearch(LightNovelRequestInput Input, Action<ILightNovelCookie> action)
        {
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                SearchResults = new LightNovelSearchResults()
                {
                    Result = new List<LightNovelSearchResult>()
                }
            };
            if (GetCookies() == null)
                action.Invoke(new LightNovelCookie());

            var SearchType = Input.Search.SearchType.ToString().ToLower();
            var KeyWord = HttpUtility.UrlEncode(Input.Search.KeyWord, Encoding.GetEncoding("GBK"));

            var response = IHttpMultiClient.HttpMulti.InitCookieContainer()
                .InitWebProxy(Input.Proxy.ToMapper<ProxyURL>())
                .Cookie(new Uri(Host), GetCookies())
                .AddNode(string.Format(Search, SearchType, KeyWord, Input.Search.Page), RequestType.GET, "GBK")
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            SyncStatic.TryCatch(() =>
            {
                document.DocumentNode.SelectNodes("//table//td/div").ForEnumerEach(node =>
                {
                    LightNovelSearchResult SearchResult = new LightNovelSearchResult();

                    var Divs = node.Descendants("div");
                    var ATag = Divs.FirstOrDefault().SelectSingleNode("a");
                    SearchResult.DetailAddress = ATag.GetAttributeValue("href", "");
                    SearchResult.BookName = ATag.GetAttributeValue("title", "");
                    SearchResult.Cover = Divs.FirstOrDefault().SelectSingleNode("a/img").GetAttributeValue("src", "");
                    var Info = Divs.LastOrDefault().Descendants("p").FirstOrDefault().InnerText;
                    SearchResult.Author = Info.Split("/")[0].Split(":").LastOrDefault();
                    SearchResult.Category = Info.Split("/")[1].Split(":").LastOrDefault();
                    SearchResult.Stutas = Info.Split("/")[2].Split(":").LastOrDefault();
                    var Desc = Divs.LastOrDefault().Descendants("p").ToArray()[1].InnerText;
                    SearchResult.Description = Desc.Split(":").LastOrDefault().Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "");

                    Result.SearchResults.Result.Add(SearchResult);
                });
            }, ex => throw new Exception("不支持全字检索"));
            Result.SearchResults.TotalPage = document.DocumentNode.SelectSingleNode("//em[@id='pagestats']").InnerText.Split("/").LastOrDefault().AsInt();
            return Result;
        }

        public LightNovelResponseOutput LightNovelCategory(LightNovelRequestInput Input, Action<ILightNovelCookie> action)
        {

            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                SingleCategoryResult = new LightNovelSingleCategoryResult()
                {
                    Result = new List<LightNovelSingleCategoryResults>()
                }
            };
            if (GetCookies() == null)
                action.Invoke(new LightNovelCookie());

            var response = IHttpMultiClient.HttpMulti.InitCookieContainer()
                .InitWebProxy(Input.Proxy.ToMapper<ProxyURL>())
                .Cookie(new Uri(Host), GetCookies())
                .AddNode($"{Input.Category.CategoryAddress}?page={Input.Category.Page}", RequestType.GET, "GBK")
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            document.DocumentNode.SelectNodes("//table//td/div").ForEnumerEach(node =>
            {
                LightNovelSingleCategoryResults SingleResult = new LightNovelSingleCategoryResults();

                var Divs = node.Descendants("div");
                var ATag = Divs.FirstOrDefault().SelectSingleNode("a");
                SingleResult.DetailAddress = ATag.GetAttributeValue("href", "");
                SingleResult.BookName = ATag.GetAttributeValue("title", "");
                SingleResult.Cover = Divs.FirstOrDefault().SelectSingleNode("a/img").GetAttributeValue("src", "");
                var Info = Divs.LastOrDefault().Descendants("p").FirstOrDefault().InnerText;
                SingleResult.Author = Info.Split("/")[0].Split(":").LastOrDefault();
                SingleResult.Category = Info.Split("/")[1].Split(":").LastOrDefault();
                SingleResult.Stutas = Info.Split("/")[2].Split(":").LastOrDefault();
                var Desc = Divs.LastOrDefault().Descendants("p").ToArray()[1].InnerText;
                SingleResult.Description = Desc.Split(":").LastOrDefault().Trim().Replace("\r", "").Replace("\n", "").Replace("\t", "");

                Result.SingleCategoryResult.Result.Add(SingleResult);
            });
            Result.SingleCategoryResult.TotalPage = document.DocumentNode.SelectSingleNode("//em[@id='pagestats']").InnerText.Split("/").LastOrDefault().AsInt();
            return Result;
        }

        public LightNovelResponseOutput LightNovelDetail(LightNovelRequestInput Input, Action<ILightNovelCookie> action)
        {
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                DetailResult = new LightNovelDetailResult()
            };

            if (GetCookies() == null)
                action.Invoke(new LightNovelCookie());

            var response = IHttpMultiClient.HttpMulti.InitCookieContainer()
                .InitWebProxy(Input.Proxy.ToMapper<ProxyURL>())
                .Cookie(new Uri(Host), GetCookies())
                .AddNode(Input.Detail.DetailAddress, RequestType.GET, "GBK")
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            document.DocumentNode.SelectNodes("//fieldset//a").ForEnumerEach(node =>
            {
                if (node.InnerText.Equals("小说目录"))
                {
                    Result.DetailResult.Address = node.GetAttributeValue("href", "");
                    Result.DetailResult.Name = node.InnerText;
                }
            });

            return Result;
        }

        public LightNovelResponseOutput LightNovelView(LightNovelRequestInput Input, Action<ILightNovelCookie> action)
        {
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                ViewResult = new List<LightNovelViewResult>()
            };

            if (GetCookies() == null)
                action.Invoke(new LightNovelCookie());

            var response = IHttpMultiClient.HttpMulti.InitCookieContainer()
                .InitWebProxy(Input.Proxy.ToMapper<ProxyURL>())
                .Cookie(new Uri(Host), GetCookies())
                .AddNode(Input.View.ViewAddress, RequestType.GET, "GBK")
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            document.DocumentNode.SelectNodes("//td[@class='ccss']//a").ForEnumerEach(node =>
            {
                var PreFix = Input.View.ViewAddress.AsSpan().Slice(0, Input.View.ViewAddress.LastIndexOf("/")).ToString();
                LightNovelViewResult view = new LightNovelViewResult
                {
                    ChapterURL = $"{PreFix}/{node.GetAttributeValue("href", "")}",
                    ChapterName = node.InnerText
                };
                Result.ViewResult.Add(view);
            });

            return Result;
        }

        public LightNovelResponseOutput LightNovelContent(LightNovelRequestInput Input)
        {
            LightNovelResponseOutput Result = new LightNovelResponseOutput
            {
                ContentResult = new LightNovelContentResult()
            };

            var response = IHttpMultiClient.HttpMulti.InitCookieContainer()
                .InitWebProxy(Input.Proxy.ToMapper<ProxyURL>())
                .AddNode(Input.Content.ChapterURL, RequestType.GET, "GBK")
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            Result.ContentResult.Content = document.DocumentNode.SelectSingleNode("//div[@id='content']")
                .InnerText.Replace("本文来自 轻小说文库(http://www.wenku8.com)", "")
                .Replace("最新最全的日本动漫轻小说 轻小说文库(http://www.wenku8.com) 为你一网打尽！", "")
                .Replace("&nbsp;", "").Trim();
            if (Result.ContentResult.Content.IsNullOrEmpty())
            {
                Result.ContentResult.Image = new List<string>();
                document.DocumentNode.SelectNodes("//div[@class='divimage']/a").ForEnumerEach(node =>
                {
                    Result.ContentResult.Image.Add(node.GetAttributeValue("href", ""));
                });
            }
            return Result;
        }
    }
}
