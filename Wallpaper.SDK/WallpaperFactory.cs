using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallpaper.SDK.ViewModel;
using Wallpaper.SDK.ViewModel.Enums;

namespace Wallpaper.SDK
{
    public class WallpaperFactory
    {
        public WallpaperRequestInput RequestParam { get; set; }
        public static WallpaperFactory Wallpaper(Action<WallpaperFactory> action)
        {
            WallpaperFactory factory = new WallpaperFactory();
            action(factory);
            if (factory.RequestParam == null)
                throw new NullReferenceException("RequestParam Is Null");
            return factory;
        }
        public WallpaperResponseOutput Runs()
        {
            IWallpaper wallpaper = new Wallpaper();
            return RequestParam.WallpaperType switch
            {
                WallpaperEnum.Init => wallpaper.WallpaperInit(RequestParam),
                WallpaperEnum.Search => wallpaper.WallpaperSearch(RequestParam),
                _ => wallpaper.WallpaperInit(RequestParam),
            };
        }
    }
}
