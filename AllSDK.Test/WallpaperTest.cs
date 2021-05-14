using XExten.Advance.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallpaper.SDK;
using Wallpaper.SDK.ViewModel;
using Wallpaper.SDK.ViewModel.Enums;
using Wallpaper.SDK.ViewModel.Request;
using System.Threading;

namespace AllSDK.Test
{
    public class WallpaperTest
    {
        public static void WallpaperAllTest()
        {
            var WallpaperInit = WallpaperFactory.Wallpaper(opt =>
              {
                  opt.RequestParam = new WallpaperRequestInput
                  {
                      WallpaperType = WallpaperEnum.Init,
                      Init = new WallpaperInit(),
                      Proxy = new WallpaperProxy()

                  };
              }).Runs();
            Console.WriteLine(WallpaperInit.ToJson());
            Thread.Sleep(1000);
            var WallpaperSearch = WallpaperFactory.Wallpaper(opt =>
            {
                opt.RequestParam = new WallpaperRequestInput
                {
                    WallpaperType = WallpaperEnum.Search,
                    Search = new WallpaperSearch()
                    {
                        KeyWord = WallpaperInit.Result.FirstOrDefault().Labels.FirstOrDefault()
                    },
                    Proxy = new WallpaperProxy()

                };
            }).Runs();
            Console.WriteLine(WallpaperSearch.ToJson());
        }
    }
}
