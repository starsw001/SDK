using HtmlAgilityPack;
using Novel.SDK.ViewModel;
using Synctool.HttpFramework;
using Synctool.LinqFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK
{
    internal class Novel : INovel
    {
        private const string Host = "https://www.bxwx.la";
        private const string Search = Host + "/ar.php?keyWord={0}";

        public NovelResponseOutput NovelInit(NovelRequestInput Input)
        {
            NovelResponseOutput Result = new NovelResponseOutput()
            {
                IndexRecommends = new List<NovelRecommend>(),
                IndexCategories = new List<NovelCategory>()
            };
            var response = HttpMultiClient.HttpMulti.AddNode(Host).Build().RunString().FirstOrDefault();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            //分类
            List<string> TempCategory = new List<string>
            {
             "玄幻奇幻","武侠仙侠","都市言情","历史军事","科幻灵异","网游竞技","女生频道"
            };
            document.DocumentNode.SelectNodes("//ul[@class='nav']//li/a").ForEnumerEach(node =>
            {

                if (TempCategory.Contains(node.InnerText))
                {
                    NovelCategory Category = new NovelCategory();
                    Category.CategoryName = node.InnerText;
                    Category.CollectAddress = Host + node.GetAttributeValue("href", "");
                    Result.IndexCategories.Add(Category);
                }
            });
            //推荐
            document.DocumentNode.SelectNodes("//div[@class='layout']//div[@class='tp-box']").ForEnumerEach(node =>
            {
                NovelRecommend RecommendOut = new NovelRecommend();
                RecommendOut.RecommendType = node.SelectSingleNode("h2").InnerText;
                node.SelectNodes("ul/li").ForEnumerEach(child =>
                {
                    NovelRecommend Recommend = new NovelRecommend();
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
                SearchResults = new List<NovelSearch>()
            };
            var response = HttpMultiClient.HttpMulti.AddNode(string.Format(Search, Input.NovelSearchKeyWord)).Build().RunString().FirstOrDefault();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            var TempResult = document.DocumentNode.SelectNodes("//ul[contains(@class,'txt-list txt-list-row5')]/li").ToList();
            TempResult.RemoveAt(0);
            TempResult.ForEach(node =>
            {
                NovelSearch search = new NovelSearch
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
                SingleCategories = new NovelSingleCategory()
            };
            if (Input.Page > 1)
                Input.NovelCategoryAddress = $"{Input.NovelCategoryAddress.Substring(0, Input.NovelCategoryAddress.LastIndexOf("/"))}/{Input.Page}.htm";
            var response = HttpMultiClient.HttpMulti.AddNode(Input.NovelCategoryAddress).Build().RunString().FirstOrDefault();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            Result.SingleCategories.TotalPage = document.DocumentNode.SelectSingleNode("//ul[contains(@class,'pagination pagination-mga')]//span")
                .InnerText.Split("/").LastOrDefault().AsInt();
            Result.SingleCategories.NovelSingles = new List<NovelSingleCategories>();
            document.DocumentNode.SelectNodes("//ul[contains(@class,'txt-list txt-list-row5')]/li").ForEnumerEach(node =>
            {
                NovelSingleCategories categories = new NovelSingleCategories
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
                Details = new NovelDetail()
            };
            Result.Details.ShortURL = Input.NovelDetailAddress;
            if (Input.Page > 1)
                Input.NovelDetailAddress = $"{Result.Details.ShortURL}index_{Input.Page}.html";
            var response = HttpMultiClient.HttpMulti.AddNode(Input.NovelDetailAddress).Build().RunString().FirstOrDefault();
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

            Result.Details.Details = new List<NovelDetails>();
            document.DocumentNode.SelectNodes("//ul[@class='section-list fix']")
                .LastOrDefault().SelectNodes("li").ForEnumerEach(node =>
                {
                    NovelDetails nd = new NovelDetails
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
                Contents = new NovelContent()
            };
            var response = HttpMultiClient.HttpMulti.AddNode(Input.NovelViewAddress).Build().RunString().FirstOrDefault();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(response);
            Result.Contents.ChapterName = document.DocumentNode.SelectSingleNode("//h1[@class='title']").InnerText;
            Result.Contents.Content = document.DocumentNode.SelectSingleNode("//div[@class='content']")
                .InnerText.Replace("章节错误,点此举报(免注册),举报后维护人员会在两分钟内校正章节内容,请耐心等待,并刷新页面。", "").Trim();
            return Result;
        }
    }
}
