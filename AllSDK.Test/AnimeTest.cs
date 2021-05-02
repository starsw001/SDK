using Anime.SDK;
using Anime.SDK.ViewModel;
using Anime.SDK.ViewModel.Request;
using Anime.SDK.ViewModel.Enums;
using XExten.Advance.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AllSDK.Test
{
    public class AnimeTest
    {
        /// <summary>
        /// 动漫测试
        /// </summary>
        public static void AnimeAllTest()
        {
            //初始化
            var AnimeInit = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Init,
                    Proxy = new AnimeProxy
                    {
                        IP = "203.74.120.79",
                        Port = 3128
                    }
                };
            }).Runs();
            Console.WriteLine(AnimeInit.ToJson());
            Thread.Sleep(1000);
            //搜索
            var AnimeSearch = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Search,
                    Proxy = new AnimeProxy(),
                    Search = new AnimeSearch
                    {
                        AnimeSearchKeyWord = "盾之勇者"
                    }
                };
            }).Runs();
            Console.WriteLine(AnimeSearch.ToJson());
            Thread.Sleep(1000);
            //分类
            var AnimeCate = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Category,
                    Proxy = new AnimeProxy(),
                    Category = new AnimeCategory
                    {
                        AnimeLetterType = AnimeLetterEnum.A
                    }
                };
            }).Runs();
            Console.WriteLine(AnimeCate.ToJson());
            Thread.Sleep(1000);
            //详情页
            var AnimeDetail = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Detail,
                    Proxy = new AnimeProxy(),
                    Detail = new AnimeDetail
                    {
                        DetailAddress = AnimeCate.SeachResults.Searchs.FirstOrDefault().DetailAddress
                    }
                };
            }).Runs();
            Console.WriteLine(AnimeDetail.ToJson());
            Thread.Sleep(1000);
            //播放页
            var AnimeWath = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Watch,
                    Proxy = new AnimeProxy(),
                    WatchPlay = new AnimeWatchPlay
                    {
                        DetailResult = AnimeDetail.DetailResults.FirstOrDefault()
                    }
                };
            }).Runs();
            Console.WriteLine(AnimeWath.ToJson());
            Thread.Sleep(1000);
        }
    }
}
