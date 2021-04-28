using Novel.SDK;
using Novel.SDK.ViewModel;
using Novel.SDK.ViewModel.Enums;
using Synctool.LinqFramework;
using Novel.SDK.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
            Thread.Sleep(1000);
            //搜索
            var NovelSearch = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Search,
                    Search = new NovelSearch
                    {
                        NovelSearchKeyWord = "神墓"
                    }
                };
            }).Runs();
            Console.WriteLine(NovelSearch.ToJson());
            Thread.Sleep(1000);
            //分类
            var NovelCate = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Category,
                    Category = new NovelCategory
                    {
                        NovelCategoryAddress = NovelInit.IndexCategories.FirstOrDefault().CollectAddress
                    }
                };
            }).Runs();
            Console.WriteLine(NovelCate.ToJson());
            Thread.Sleep(1000);
            //详情
            var NovelDetail = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Detail,
                    Detail = new NovelDetail
                    {
                        NovelDetailAddress = NovelCate.SingleCategories.NovelSingles.FirstOrDefault().DetailAddress
                    }
                };
            }).Runs();
            Console.WriteLine(NovelDetail.ToJson());
            Thread.Sleep(1000);
            //内容
            var NovelContent = NovelFactory.Novel(opt =>
            {
                opt.RequestParam = new NovelRequestInput
                {
                    NovelType = NovelEnum.Watch,
                    View = new NovelView
                    {
                        NovelViewAddress = NovelDetail.Details.Details.FirstOrDefault().ChapterURL
                    }
                };
            }).Runs();
            Console.WriteLine(NovelContent.ToJson());
            Thread.Sleep(1000);
        }
    }
}
