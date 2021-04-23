using Novel.SDK;
using Novel.SDK.ViewModel;
using Synctool.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSDK.Test
{
    public class NovelTest
    {
        /// <summary>
        /// 小说测试
        /// </summary>
        public static void NovelAllTest()
        {
            //初始化
            var NovelInit = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Init
                };
            }).Runs();
            Console.WriteLine(NovelInit.ToJson());
            //搜索
            var NovelSearch = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Search,
                    NovelSearchKeyWord = "神墓"
                };
            }).Runs();
            Console.WriteLine(NovelSearch.ToJson());
            //分类
            var NovelCate = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Category,
                    NovelCategoryAddress = NovelInit.IndexCategories.FirstOrDefault().CollectAddress
                };
            }).Runs();
            Console.WriteLine(NovelCate.ToJson());
            //详情
            var NovelDetail = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Detail,
                    NovelDetailAddress = NovelCate.SingleCategories.NovelSingles.FirstOrDefault().DetailAddress
                };
            }).Runs();
            Console.WriteLine(NovelDetail.ToJson());
            //内容
            var NovelContent = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Watch,
                    NovelViewAddress = NovelDetail.Details.Details.FirstOrDefault().ChapterURL
                };
            }).Runs();
            Console.WriteLine(NovelContent.ToJson());
        }
    }
}
