using LightNovel.SDK;
using LightNovel.SDK.ViewModel;
using LightNovel.SDK.ViewModel.Enums;
using LightNovel.SDK.ViewModel.Request;
using Synctool.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllSDK.Test
{
    public class LightNovelTest
    {
        /// <summary>
        /// 轻小说测试
        /// </summary>
        public static void LightNovelAllTest()
        {
            //初始化
            var LightNovelInit = LightNovelFactory.LightNovel(opt =>
             {
                 opt.RequestParam = new LightNovelRequestInput
                 {
                     LightNovelType = LightNovelEnum.Init,
                 };
             }).Runs(Light =>
             {
                 Light.RefreshCookie(new LightNovelRefresh());
             });
            Console.WriteLine(LightNovelInit.ToJson());
            //搜索
            var LightNovelSearch = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Search,
                    Search = new LightNovelSearch
                    {
                        KeyWord = "异世界",
                        SearchType = LightNovelSearchEnum.ArticleName
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh());
            });
            Console.WriteLine(LightNovelSearch.ToJson());
            //分类
            var LightNovelCate = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Category,
                    Category = new  LightNovelCategory
                    {
                       CategoryAddress= LightNovelInit.CategoryResults.FirstOrDefault().CategoryAddress
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh());
            });
            Console.WriteLine(LightNovelCate.ToJson());
        }
    }
}
