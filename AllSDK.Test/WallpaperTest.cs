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
        }
    }
}
