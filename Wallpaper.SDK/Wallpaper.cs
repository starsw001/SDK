using Synctool.HttpFramework.MultiCommon;
using Synctool.HttpFramework.MultiFactory;
using Synctool.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallpaper.SDK.ViewModel;

namespace Wallpaper.SDK
{
    internal class Wallpaper : IWallpaper
    {
        private const string Host = "https://konachan.com";
        private const string All = Host + "/post.json?page={0}&limit={1}";
        private const string Search = All + "?tags={2}";

        public WallpaperResponseOutput WallpaperInit(WallpaperRequestInput Input)
        {
            WallpaperResponseOutput Result = new WallpaperResponseOutput();

            var response = IHttpMultiClient.HttpMulti.InitWebProxy(Input.Proxy.ToMapper<ProxyURL>())
                 .AddNode(string.Format(All, Input.Init.Page, Input.Init.Limit))
                 .Build(UseHttps:true).RunString().FirstOrDefault();


            return Result;
        }
    }
}
