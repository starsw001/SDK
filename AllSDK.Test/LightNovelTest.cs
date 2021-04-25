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
            var LightNovelInit = LightNovelFactory.Novel(opt =>
             {
                 opt.RequestParam = new LightNovelRequestInput
                 {
                     LightNovelType = LightNovelEnum.Init,
                     Init = new LightNovelInit()
                 };
             }).Runs();
            var LightNovelSearch = LightNovelFactory.Novel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Search,
                    Search = new LightNovelSearch
                    {
                        KeyWord = "恶魔高校DxD(High School DxD)",
                        SearchType = LightNovelSearchEnum.ArticleName
                    }
                };
            }).Runs();
            Console.WriteLine(LightNovelInit.ToJson());
        }
    }
}
