using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallpaper.SDK.ViewModel.Enums;
using Wallpaper.SDK.ViewModel.Request;

namespace Wallpaper.SDK.ViewModel
{
    public class WallpaperRequestInput
    {
        /// <summary>
        /// 模式
        /// </summary>
        public WallpaperEnum WallpaperType { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        public WallpaperProxy Proxy { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        public WallpaperInit Init { get; set; }
    } 
}
