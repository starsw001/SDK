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
            document.DocumentNode.SelectNodes("//div[@class='layout']//div[@class='tp-box']").ForEnumerEach((Action<HtmlNode>)(node =>
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
            }));
            return Result;
        }
    }
}
