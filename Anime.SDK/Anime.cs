using Anime.SDK.ViewModel;
using Synctool.HttpFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using Synctool.LinqFramework;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;

namespace Anime.SDK
{
    internal class Anime : IAnime
    {
        private const string Host = "http://www.cgdm.net";
        private const string Search = Host + "/search.asp?searchword={0}&page={1}";
        private const string Category = Search + "&searchtype=4";
        public Anime()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
        /*
         * step1:执行初始化
         * step2:执行搜索或者分类
         * step3:执行详情
         * step4:执行观看
         */
        public AnimeResponseOutput AnimeInit(AnimeRequstInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                WeekDays = new List<AnimeWeekDay>(),
                RecommendCategory = new Dictionary<string, string>()
            };
            var bytes = HttpMultiClient.HttpMulti.AddNode(Host).Build().RunBytes().FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(reader.ReadToEnd());

            string Ruler = "//div[@class='page_content']//div[@id='brood']/div[contains(@id,'anime') and not(contains(@id,'anime_w0'))]";
            document.DocumentNode.SelectNodes(Ruler).ForEnumerEach(item =>
            {
                AnimeWeekDay WeekDay = new AnimeWeekDay
                {
                    DayName = item.SelectSingleNode("div[@class='title_week']").InnerText,
                    DayRecommends = new List<AnimeWeekDayRecommend>()
                };
                item.Descendants("a").ForEnumerEach(items =>
                {
                    AnimeWeekDayRecommend DayRecommend = new AnimeWeekDayRecommend
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
        public AnimeResponseOutput AnimeSearch(AnimeRequstInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                SeachResults = new List<AnimeSearchResult>()
            };
            string KeyWord = HttpUtility.UrlEncode(Input.AnimeSearchKeyWord, Encoding.GetEncoding("GBK"));
            var bytes = HttpMultiClient.HttpMulti.AddNode(string.Format(Search, KeyWord, Input.Page)).Build().RunBytes().FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(reader.ReadToEnd());
            var content = document.DocumentNode.SelectSingleNode("//div[@class='movie-chrList']");
            var totalpage = Regex.Match(content.Descendants("span").ToList()[1].InnerText.Split("/")[1], "\\d+").Value.AsInt();
            content.SelectNodes("ul/li/div[@class='cover']/a").ToList().ForEach(item =>
            {
                AnimeSearchResult SearchResult = new AnimeSearchResult
                {
                    DetailAddress = Host + item.GetAttributeValue("href", "")
                };
                var img = item.SelectSingleNode("img");
                SearchResult.AnimeName = img.GetAttributeValue("alt", "");
                SearchResult.AnimeCover = Host + img.GetAttributeValue("src", "");
                Result.SeachResults.Add(SearchResult);
            });
            return Result;
        }
        public AnimeResponseOutput AnimeCategory(AnimeRequstInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                SeachResults = new List<AnimeSearchResult>()
            };
            var bytes = HttpMultiClient.HttpMulti.AddNode(string.Format(Category, Input.AnimeLetterType.ToString(), Input.Page)).Build().RunBytes().FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(reader.ReadToEnd());
            var content = document.DocumentNode.SelectSingleNode("//div[@class='movie-chrList']");
            var totalpage = Regex.Match(content.Descendants("span").ToList()[1].InnerText.Split("/")[1], "\\d+").Value.AsInt();
            content.SelectNodes("ul/li/div[@class='cover']/a").ToList().ForEach(item =>
            {
                AnimeSearchResult SearchResult = new AnimeSearchResult
                {
                    DetailAddress = Host + item.GetAttributeValue("href", "")
                };
                var img = item.SelectSingleNode("img");
                SearchResult.AnimeName = img.GetAttributeValue("alt", "");
                SearchResult.AnimeCover = Host + img.GetAttributeValue("src", "");
                Result.SeachResults.Add(SearchResult);
            });
            return Result;
        }
        public AnimeResponseOutput AnimeDetail(AnimeRequstInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput()
            {
                DetailResults = new List<AnimeDetailResult>()
            };
            var bytes = HttpMultiClient.HttpMulti.AddNode(Input.DetailAddress).Build().RunBytes().FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(reader.ReadToEnd());
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
        public AnimeResponseOutput AnimeWatchPlay(AnimeRequstInput Input)
        {
            AnimeResponseOutput Result = new AnimeResponseOutput();

            if (Input.DetailResult == null)
                throw new NullReferenceException($"{nameof(AnimeDetailResult)} Is Null");
            if (Input.DetailResult.IsDownURL)
                return Result;
            var bytes = HttpMultiClient.HttpMulti.AddNode(Input.DetailResult.WatchAddress).Build().RunBytes().FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(reader.ReadToEnd());
            var JsHost = Host + document.DocumentNode.SelectSingleNode("//div[@class='play-box']//script").GetAttributeValue("src", "");

            WebClient client = new WebClient();
            var spans = Regex.Unescape(client.DownloadString(JsHost)).Split("urlinfo")[0].Split("=")[1].AsSpan();
            Dictionary<string, string> Map = new Dictionary<string, string>();
            spans[2..^4].ToString().Split("[")[1].Split(",").ForArrayEach<string>(item =>
            {
                var temp = item.Split("?").FirstOrDefault().Split("$");
                Map.Add(temp[0].Replace("'", ""), temp[1]);
            });
            Result.PlayURL = Map[Input.DetailResult.CollectName];
            return Result;
        }
    }
}
