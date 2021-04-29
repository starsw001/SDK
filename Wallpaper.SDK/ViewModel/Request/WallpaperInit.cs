using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallpaper.SDK.ViewModel.Request
{
    public class WallpaperInit
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
    }
}
