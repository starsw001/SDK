using Anime.SDK;
using Anime.SDK.ViewModel;
using Synctool.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    AnimeType = AnimeEnum.Init
                };
            }).Runs();
            Console.WriteLine(AnimeInit.ToJson());
            //搜索
            var AnimeSearch = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Search,
                    AnimeSearchKeyWord = "盾之勇者"
                };
            }).Runs();
            Console.WriteLine(AnimeSearch.ToJson());
            //分类
            var AnimeCate = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Category,
                    AnimeLetterType = AnimeLetterEnum.A
                };
            }).Runs();
            Console.WriteLine(AnimeCate.ToJson());
            //详情页
            var AnimeDetail = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Detail,
                    DetailAddress = AnimeCate.SeachResults.Searchs.FirstOrDefault().DetailAddress
                };
            }).Runs();
            Console.WriteLine(AnimeDetail.ToJson());
            //播放页
            var AnimeWath = AnimeFactory.Anime(opt =>
            {
                opt.RequestParam = new AnimeRequestInput
                {
                    AnimeType = AnimeEnum.Watch,
                    DetailResult = AnimeDetail.DetailResults.FirstOrDefault()
                };
            }).Runs();
            Console.WriteLine(AnimeWath.ToJson());
        }
    }
}
