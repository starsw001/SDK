using LightNovel.SDK;
using LightNovel.SDK.ViewModel;
using LightNovel.SDK.ViewModel.Enums;
using LightNovel.SDK.ViewModel.Request;
using XExten.Advance.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                     Proxy= new LightNovelProxy()
                 };
             }).Runs(Light =>
             {
                 Light.RefreshCookie(new LightNovelRefresh
                 {
                     UserName = "kilydoll365",
                     PassWord = "sion8550"
                 }, new LightNovelProxy());
             });
            Console.WriteLine(LightNovelInit.ToJson());
            Thread.Sleep(1000);
            //搜索
            var LightNovelSearch = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Search,
                    Proxy = new LightNovelProxy(),
                    Search = new LightNovelSearch
                    {
                        KeyWord = "恶魔高校",
                        SearchType = LightNovelSearchEnum.ArticleName
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh
                {
                    UserName = "kilydoll365",
                    PassWord = "sion8550"
                }, new LightNovelProxy());
            });
            Console.WriteLine(LightNovelSearch.ToJson());
            Thread.Sleep(1000);
            //分类
            var LightNovelCate = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Category,
                    Proxy = new LightNovelProxy(),
                    Category = new LightNovelCategory
                    {
                        CategoryAddress = LightNovelInit.CategoryResults.FirstOrDefault().CategoryAddress
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh
                {
                    UserName = "kilydoll365",
                    PassWord = "sion8550"
                }, new LightNovelProxy());
            });
            Console.WriteLine(LightNovelCate.ToJson());
            Thread.Sleep(1000);
            //详情
            var LightNovelDetail = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Detail,
                    Proxy = new LightNovelProxy(),
                    Detail = new LightNovelDetail
                    {
                        DetailAddress = LightNovelSearch.SearchResults.Result.LastOrDefault().DetailAddress
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh
                {
                    UserName = "kilydoll365",
                    PassWord = "sion8550"
                }, new LightNovelProxy());
            });
            Console.WriteLine(LightNovelDetail.ToJson());
            Thread.Sleep(1000);
            //预览
            var LightNovelView = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.View,
                    Proxy = new LightNovelProxy(),
                    View = new LightNovelView
                    {
                        ViewAddress = LightNovelDetail.DetailResult.Address,
                    }
                };
            }).Runs(Light =>
            {
                Light.RefreshCookie(new LightNovelRefresh
                {
                    UserName = "kilydoll365",
                    PassWord = "sion8550"
                },new LightNovelProxy());
            });
            Console.WriteLine(LightNovelView.ToJson());
            Thread.Sleep(1000);
            //内容
            var LightNovelContent = LightNovelFactory.LightNovel(opt =>
            {
                opt.RequestParam = new LightNovelRequestInput
                {
                    LightNovelType = LightNovelEnum.Content,
                    Proxy = new LightNovelProxy(),
                    Content = new LightNovelContent
                    {
                        ChapterURL = LightNovelView.ViewResult[7].ChapterURL,
                    }
                };
            }).Runs();
            Console.WriteLine(LightNovelContent.ToJson());
            Thread.Sleep(1000);
        }
    }
}
