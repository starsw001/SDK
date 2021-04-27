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
            //var LightNovelInit = LightNovelFactory.LightNovel(opt =>
            // {
            //     opt.RequestParam = new LightNovelRequestInput
            //     {
            //         LightNovelType = LightNovelEnum.Init,
            //     };
            // }).Runs(Light =>
            // {
            //     Light.RefreshCookie(new LightNovelRefresh());
            // });
            //Console.WriteLine(LightNovelInit.ToJson());
            //搜索
            var LightNovelSearch = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Search,
                    Search = new LightNovelSearch
                    {
                        KeyWord = "恶魔高校",
                        SearchType = LightNovelSearchEnum.ArticleName
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh());
            });
            Console.WriteLine(LightNovelSearch.ToJson());
            //分类
            //var LightNovelCate = LightNovelFactory.LightNovel(opt =>
            //{
            //    opt.RequestParam = new LightNovelRequestInput
            //    {
            //        LightNovelType = LightNovelEnum.Category,
            //        Category = new  LightNovelCategory
            //        {
            //           CategoryAddress= LightNovelInit.CategoryResults.FirstOrDefault().CategoryAddress
            //        }
            //    };
            //}).Runs(Light =>
            //{
            //    Light.RefreshCookie(new LightNovelRefresh());
            //});
            //Console.WriteLine(LightNovelCate.ToJson());
            //详情
            var LightNovelDetail = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Detail,
                    Detail = new  LightNovelDetail
                    {
                       DetailAddress = LightNovelSearch.SearchResults.Result.LastOrDefault().DetailAddress
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh());
            });
            Console.WriteLine(LightNovelDetail.ToJson());
            //预览
            var LightNovelView = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.View,
                    View = new  LightNovelView
                    {
                        ViewAddress = LightNovelDetail.DetailResult.Address,
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh());
            });
            Console.WriteLine(LightNovelView.ToJson());
            //内容
            var LightNovelContent = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Content,
                    Content = new  LightNovelContent
                    {
                         ChapterURL = LightNovelView.ViewResult.FirstOrDefault().ChapterURL,
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh());
            });
            Console.WriteLine(LightNovelContent.ToJson());
        }
    }
}
