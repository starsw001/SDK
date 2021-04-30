using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallpaper.SDK.ViewModel;

namespace Wallpaper.SDK
{
    public interface IWallpaper
    {
        WallpaperResponseOutput WallpaperInit(WallpaperRequestInput Input);

        WallpaperResponseOutput WallpaperSearch(WallpaperRequestInput Input);
    }
}
