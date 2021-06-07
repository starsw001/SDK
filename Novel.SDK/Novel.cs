using HtmlAgilityPack;
using Novel.SDK.ViewModel;
using Novel.SDK.ViewModel.Response;
using XExten.Advance.HttpFramework;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novel.SDK.ViewModel.Request;

namespace Novel.SDK
{
    internal class Novel : INovel
    {
        private const string Host = "https://www.bxwx.la";
        private const string Search = Host + "/ar.php?keyWord={0}";
        /*
       * step1:执行初始化
       * step2:执行搜索或者分类
       * step3:执行详情
       * step4:执行浏览
       */
        public NovelResponseOutput NovelInit(NovelRequestInput Input)
        {
            NovelResponseOutput Result = new NovelResponseOutput()
            {
                IndexRecommends = new List<NovelRecommendResult>(),
                IndexCategories = new List<NovelCategoryResult>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new NovelProxy()).ToMapper<MultiProxy>())
                .AddNode(opt=>opt.NodePath= Host).Build().RunString().FirstOrDefault();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            //分类
            List<string> FilterCategory = new List<string>
            {
             "玄幻奇幻","武侠仙侠","都市言情","历史军事","科幻灵异","网游竞技","女生频道"
            };
            document.DocumentNode.SelectNodes("//ul[@class='nav']//li/a").ForEnumerEach(node =>
            {
                if (FilterCategory.Contains(node.InnerText))
                {
                    NovelCategoryResult Category = new NovelCategoryResult();
                    Category.CategoryName = node.InnerText;
                    Category.CollectAddress = Host + node.GetAttributeValue("href", "");
                    Result.IndexCategories.Add(Category);
                }
            });
            //推荐
            document.DocumentNode.SelectNodes("//div[@class='layout']//div[@class='tp-box']").ForEnumerEach(node =>
            {
                NovelRecommendResult RecommendOut = new NovelRecommendResult();
                RecommendOut.RecommendType = node.SelectSingleNode("h2").InnerText;
                node.SelectNodes("ul/li").ForEnumerEach(child =>
                {
                    NovelRecommendResult Recommend = new NovelRecommendResult();
                    Recommend.RecommendType = RecommendOut.RecommendType;
                    Recommend.DetailAddress = Host + child.SelectSingleNode("a").GetAttributeValue("href", "");
                    Recommend.BookName = child.SelectSingleNode("a").InnerText;
                    Recommend.Author = child.InnerText.Split("/").LastOrDefault();
                    Result.IndexRecommends.Add(Recommend);
                });
                RecommendOut.BookName = node.SelectSingleNode("div[@class='top']//dt/a").InnerText;
                RecommendOut.Author = "佚名";
                RecommendOut.DetailAddress = Host + node.SelectSingleNode("div[@class='top']//dt/a").GetAttributeValue("href", "");
                Result.IndexRecommends.Add(RecommendOut);
            });
            return Result;
        }

        public NovelResponseOutput NovelSearch(NovelRequestInput Input)
        {
            NovelResponseOutput Result = new NovelResponseOutput()
            {
                SearchResults = new List<NovelSearchResult>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new NovelProxy()).ToMapper<MultiProxy>())
                .AddNode(opt=>opt.NodePath= string.Format(Search, Input.Search.NovelSearchKeyWord))
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            var TempResult = document.DocumentNode.SelectNodes("//ul[contains(@class,'txt-list txt-list-row5')]/li").ToList();
            TempResult.RemoveAt(0);

            TempResult.ForEach(node =>
            {
                NovelSearchResult search = new NovelSearchResult
                {
                    RecommendType = node.SelectSingleNode("span[@class='s1']").InnerText,
                    Author = node.SelectSingleNode("span[@class='s4']").InnerText,
                    DetailAddress = Host + node.SelectSingleNode("span[@class='s2']/a").GetAttributeValue("href", ""),
                    BookName = node.SelectSingleNode("span[@class='s2']/a").InnerText,
                    UpdateDate = node.SelectSingleNode("span[@class='s5']").InnerText
                };
                Result.SearchResults.Add(search);
            });
            return Result;
        }

        public NovelResponseOutput NovelCategory(NovelRequestInput Input)
        {
            NovelResponseOutput Result = new NovelResponseOutput()
            {
                SingleCategories = new NovelSingleCategoryResult()
            };

            if (Input.Category.Page > 1)
                Input.Category.NovelCategoryAddress = @$"{Input.Category.NovelCategoryAddress
                    .Substring(0, Input.Category.NovelCategoryAddress.LastIndexOf("/"))}/{Input.Category.Page}.htm";

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new NovelProxy()).ToMapper<MultiProxy>())
                .AddNode(opt=>opt.NodePath= Input.Category.NovelCategoryAddress)
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            Result.SingleCategories.TotalPage = document.DocumentNode.SelectSingleNode("//ul[contains(@class,'pagination pagination-mga')]//span")
                .InnerText.Split("/").LastOrDefault().AsInt();

            Result.SingleCategories.NovelSingles = new List<NovelSingleCategoryResults>();
            document.DocumentNode.SelectNodes("//ul[contains(@class,'txt-list txt-list-row5')]/li").ForEnumerEach(node =>
            {
                NovelSingleCategoryResults categories = new NovelSingleCategoryResults
                {
                    Author = node.SelectSingleNode("span[@class='s4']").InnerText,
                    DetailAddress = Host + node.SelectSingleNode("span[@class='s2']/a").GetAttributeValue("href", ""),
                    BookName = node.SelectSingleNode("span[@class='s2']/a").InnerText,
                    UpdateDate = node.SelectSingleNode("span[@class='s5']").InnerText
                };
                Result.SingleCategories.NovelSingles.Add(categories);
            });
            return Result;
        }

        public NovelResponseOutput NovelDetail(NovelRequestInput Input)
        {
            NovelResponseOutput Result = new NovelResponseOutput()
            {
                Details = new NovelDetailResult()
            };
            Result.Details.ShortURL = Input.Detail.NovelDetailAddress;

            if (Input.Detail.Page > 1)
                Input.Detail.NovelDetailAddress = $"{Result.Details.ShortURL}index_{Input.Detail.Page}.html";

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new NovelProxy()).ToMapper<MultiProxy>())
                .AddNode(opt=>opt.NodePath= Input.Detail.NovelDetailAddress)
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            var HContent = document.DocumentNode.SelectSingleNode("//div[@class='detail-box']");

            Result.Details.Cover = HContent.SelectSingleNode("div[@class='imgbox']/img").GetAttributeValue("src", "");
            Result.Details.BookName = HContent.SelectSingleNode("div[@class='imgbox']/img").GetAttributeValue("alt", "");
            var Info = HContent.SelectNodes("div[@class='info']//div[@class='fix']/p");
            Result.Details.Author = Info.FirstOrDefault().InnerText.Split("：").LastOrDefault();
            Result.Details.BookType = Info[1].InnerText.Split("：").LastOrDefault();
            Result.Details.Status = Info[2].InnerText.Split("：").LastOrDefault();
            Result.Details.LastUpdateTime = Info[4].InnerText.Split("：").LastOrDefault().AsDateTime();
            Result.Details.Description = HContent.SelectSingleNode("div[@class='info']//div[@class='desc xs-hidden']").InnerText.Trim()
                .Replace("\r", "").Replace("\n", "").Replace("\t", "");
            Result.Details.TotalPage = document.DocumentNode.SelectNodes("//select[@name='pageselect']/option").Count;

            Result.Details.Details = new List<NovelDetailResults>();
            document.DocumentNode.SelectNodes("//ul[@class='section-list fix']")
                .LastOrDefault().SelectNodes("li").ForEnumerEach(node =>
                {
                    NovelDetailResults nd = new NovelDetailResults
                    {
                        ChapterName = node.SelectSingleNode("a").InnerText,
                        ChapterURL = Host + node.SelectSingleNode("a").GetAttributeValue("href", "")
                    };
                    Result.Details.Details.Add(nd);
                });

            return Result;
        }

        public NovelResponseOutput NovelView(NovelRequestInput Input)
        {
            NovelResponseOutput Result = new NovelResponseOutput()
            {
                Contents = new NovelContentResult()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Input.Proxy ?? new NovelProxy()).ToMapper<MultiProxy>())
                .AddNode(opt=>opt.NodePath= Input.View.NovelViewAddress)
                .Build().RunString().FirstOrDefault();

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);

            Result.Contents.ChapterName = document.DocumentNode.SelectSingleNode("//h1[@class='title']").InnerText;
            Result.Contents.Content = document.DocumentNode.SelectSingleNode("//div[@class='content']")
                .InnerText.Replace("&nbsp;", "")
                .Replace("章节错误,点此举报(免注册),举报后维护人员会在两分钟内校正章节内容,请耐心等待,并刷新页面。", "").Trim();
            return Result;
        }
    }
}
