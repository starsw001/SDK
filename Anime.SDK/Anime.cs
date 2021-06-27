using Anime.SDK.ViewModel;
using Anime.SDK.ViewModel.Response;
using HtmlAgilityPack;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Anime.SDK.ViewModel.Request;

namespace Anime.SDK
{
    internal class Anime : IAnime
    {
        private static readonly string Host = DIYHost.IsNullOrEmpty() ? "http://www.ysjdm.com" : DIYHost;
        private static readonly string Search = Host + "/search.asp?searchword={0}&page={1}";
        private static readonly string Category = Search + "&searchtype=4";
        public static string DIYHost { get; set; }
        /*
         * step1:执行初始化
         * step2:执行搜索或者分类
         * step3:执行详情
         * step4:执行观看
         */
        public AnimeResponseOutput AnimeInit(AnimeRequestInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                WeekDays = new List<AnimeWeekDayResult>(),
                RecommendCategory = new Dictionary<string, string>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new AnimeProxy()).ToMapper<MultiProxy>())
                .AddNode(opt =>
                {
                    opt.ReqType = MultiType.GET;
                    opt.NodePath = Host;
                    opt.Encoding = "GBK";
                }).Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            string Ruler = "//div[@class='page_content']//div[@id='brood']/div[contains(@id,'anime') and not(contains(@id,'anime_w0'))]";
            document.DocumentNode.SelectNodes(Ruler).ForEnumerEach(item =>
            {
                AnimeWeekDayResult WeekDay = new AnimeWeekDayResult
                {
                    DayName = item.SelectSingleNode("div[@class='title_week']").InnerText,
                    DayRecommends = new List<AnimeWeekDayRecommendResult>()
                };
                item.Descendants("a").ForEnumerEach(items =>
                {
                    AnimeWeekDayRecommendResult DayRecommend = new AnimeWeekDayRecommendResult
                    {
                        AnimeURL = items.GetAttributeValue("href", ""),
                        AnimeName = items.InnerText
                    };
                    if (!DayRecommend.AnimeURL.Contains("forum.php"))
                        WeekDay.DayRecommends.Add(DayRecommend);
                });
                Result.WeekDays.Add(WeekDay);
            });

            document.DocumentNode.SelectNodes("//div[@id='naviin']/ul")
                .LastOrDefault().Descendants("a").ForEnumerEach(item =>
                {
                    Result.RecommendCategory.Add(item.InnerText, Host + item.GetAttributeValue("href", ""));
                });

            return Result;
        }
        public AnimeResponseOutput AnimeSearch(AnimeRequestInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                SeachResults = new AnimeSearchResult()
            };
            Result.SeachResults.Searchs = new List<AnimeSearchResults>();

            string KeyWord = HttpUtility.UrlEncode(Input.Search.AnimeSearchKeyWord, Encoding.GetEncoding("GBK"));
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new AnimeProxy()).ToMapper<MultiProxy>())
                 .AddNode(opt =>
                 {
                     opt.ReqType = MultiType.GET;
                     opt.NodePath = string.Format(Search, KeyWord, Input.Search.Page);
                     opt.Encoding = "GBK";
                 }).Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            var content = document.DocumentNode.SelectSingleNode("//div[@class='movie-chrList']");
            Result.SeachResults.Page = Regex.Match(content.Descendants("span").ToList()[1].InnerText.Split("/")[1], "\\d+").Value.AsInt();

            content.SelectNodes("ul/li/div[@class='cover']/a").ToList().ForEach(item =>
            {
                AnimeSearchResults SearchResult = new AnimeSearchResults
                {
                    DetailAddress = Host + item.GetAttributeValue("href", "")
                };
                var img = item.SelectSingleNode("img");
                SearchResult.AnimeName = img.GetAttributeValue("alt", "");
                SearchResult.AnimeCover = Host + img.GetAttributeValue("src", "");
                Result.SeachResults.Searchs.Add(SearchResult);
            });
            return Result;
        }
        public AnimeResponseOutput AnimeCategory(AnimeRequestInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                SeachResults = new AnimeSearchResult()
            };
            Result.SeachResults.Searchs = new List<AnimeSearchResults>();

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new AnimeProxy()).ToMapper<MultiProxy>())
                .AddNode(opt =>
                 {
                     opt.ReqType = MultiType.GET;
                     opt.NodePath = string.Format(Category, Input.Category.AnimeLetterType.ToString(), Input.Category.Page);
                     opt.Encoding = "GBK";
                 }).Build().RunString().FirstOrDefault();


            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            var content = document.DocumentNode.SelectSingleNode("//div[@class='movie-chrList']");
            Result.SeachResults.Page = Regex.Match(content.Descendants("span").ToList()[1].InnerText.Split("/")[1], "\\d+").Value.AsInt();

            content.SelectNodes("ul/li/div[@class='cover']/a").ToList().ForEach(item =>
            {
                AnimeSearchResults SearchResult = new AnimeSearchResults
                {
                    DetailAddress = Host + item.GetAttributeValue("href", "")
                };
                var img = item.SelectSingleNode("img");
                SearchResult.AnimeName = img.GetAttributeValue("alt", "");
                SearchResult.AnimeCover = Host + img.GetAttributeValue("src", "");
                Result.SeachResults.Searchs.Add(SearchResult);
            });
            return Result;
        }
        public AnimeResponseOutput AnimeDetail(AnimeRequestInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                DetailResults = new List<AnimeDetailResult>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new AnimeProxy()).ToMapper<MultiProxy>())
                .AddNode(opt =>
                {
                    opt.ReqType = MultiType.GET;
                    opt.NodePath = Input.Detail.DetailAddress;
                    opt.Encoding = "GBK";
                }).Build().RunString().FirstOrDefault();


            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            document.DocumentNode.SelectNodes("//div[@class='playurl']//ul//a").ForEnumerEach(item =>
            {
                var uri = item.GetAttributeValue("href", "");
                Result.DetailResults.Add(new AnimeDetailResult
                {
                    WatchAddress = !uri.Contains("magnet:?xt=urn:btih:") ? Host + uri : uri,
                    IsDownURL = uri.Contains("magnet:?xt=urn:btih:"),
                    CollectName = item.InnerText
                });
            });
            return Result;
        }
        public AnimeResponseOutput AnimeWatchPlay(AnimeRequestInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput();
            if (Input.WatchPlay.DetailResult == null)
                throw new NullReferenceException($"{nameof(AnimeDetailResult)} Is Null");
            if (Input.WatchPlay.DetailResult.IsDownURL)
                return Result;

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new AnimeProxy()).ToMapper<MultiProxy>())
                .AddNode(opt =>
                 {
                     opt.ReqType = MultiType.GET;
                     opt.NodePath = Input.WatchPlay.DetailResult.WatchAddress;
                     opt.Encoding = "GBK";
                 }).Build().RunString().FirstOrDefault();


            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            var JsHost = Host + document.DocumentNode.SelectSingleNode("//div[@class='play-box']//script").GetAttributeValue("src", "");

            WebClient client = new WebClient();
            var spans = Regex.Unescape(client.DownloadString(JsHost)).Split("urlinfo")[0].Split("=")[1].AsSpan();
            Dictionary<string, string> Map = new Dictionary<string, string>();
            spans[2..^4].ToString().Split("[")[1].Split(",").ForArrayEach<string>(item =>
            {
                var temp = item.Split("?").FirstOrDefault().Split("$");
                Map.Add(temp[0].Replace("'", ""), temp[1]);
            });
            Result.PlayURL = Map[Input.WatchPlay.DetailResult.CollectName];
            return Result;
        }
    }
}
