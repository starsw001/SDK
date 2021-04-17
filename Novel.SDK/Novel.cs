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
        private const string Host = "https://m.bqkan8.com";

        public Novel()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public NovelResponseOutput NovelInit(NovelRequestInput Input)
        {
            NovelResponseOutput Result = new NovelResponseOutput()
            {
                IndexHots = new List<NovelHot>(),
                IndexRecommends = new List<NovelRecommend>(),
                IndexCategories = new List<NovelCategory>()
            };
            var bytes = HttpMultiClient.HttpMulti.AddNode(Host).Build().RunBytes().FirstOrDefault();
            using StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.GetEncoding("GBK"));
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(reader.ReadToEnd());
            //分类
            document.DocumentNode.SelectNodes("//div[@class='nav']//li/a").ForEnumerEach(node =>
            {
                if (!node.InnerText.Equals("首页"))
                {
                    NovelCategory Category = new NovelCategory();
                    Category.CategoryName = node.InnerText;
                    Category.CollectAddress = Host + node.GetAttributeValue("href", "");
                    Result.IndexCategories.Add(Category);
                }
            });
            //热搜
            document.DocumentNode.SelectNodes("//div[@class='hot']//div[@class='p10']").ForEnumerEach(node =>
            {
                NovelHot Hot = new NovelHot();

                var a_node = node.SelectSingleNode("div[@class='image']/a");
                Hot.DetailAddress = Host + a_node.GetAttributeValue("href", "");

                var img_node = a_node.SelectSingleNode("img");
                Hot.Cover = img_node.GetAttributeValue("src", "");
                Hot.BookName = img_node.GetAttributeValue("alt", "");

                Hot.Author = node.SelectSingleNode("dl//span").InnerText;
                Hot.Description = node.SelectSingleNode("dl/dd").InnerText
                .Trim().Replace("\n", "").Replace("\r", "").Replace("\t", "");

                Result.IndexHots.Add(Hot);
            });
            //推荐
            document.DocumentNode.SelectNodes("//div[@class='block']/ul[@class='lis']/li").ForEnumerEach(node =>
            {
                NovelRecommend Recommend = new NovelRecommend();

                Recommend.RecommendType = node.SelectSingleNode("span[@class='s1']").InnerText;
                Recommend.BookName = node.SelectSingleNode("span[@class='s2']/a").InnerText;
                Recommend.DetailAddress = Host + node.SelectSingleNode("span[@class='s2']/a").GetAttributeValue("href", "");
                Recommend.Author = node.SelectSingleNode("span[@class='s3']").InnerText;

                Result.IndexRecommends.Add(Recommend);
            });

            return Result;
        }
    }
}
