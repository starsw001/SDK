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
                    Search = new LightNovelSearch(LightNovelSearchEnum.ArticleName, "恶魔高校")
                };
            }).Runs();
            Console.WriteLine(LightNovelInit.ToJson());
        }
    }
}
