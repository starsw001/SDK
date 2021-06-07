using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;
using System.Collections.Generic;
using System.Linq;
using Wallpaper.SDK.ViewModel;
using Wallpaper.SDK.ViewModel.Response;
using System;
using Newtonsoft.Json.Linq;
using System.Web;
using Wallpaper.SDK.ViewModel.Request;

namespace Wallpaper.SDK
{
    internal class Wallpaper : IWallpaper
    {
        private const string Host = "https://konachan.com";
        private const string All = Host + "/post.json?page={0}&limit={1}";
        private const string Search = All + "?tags={2}";

        public WallpaperResponseOutput WallpaperInit(WallpaperRequestInput Input)
        {
            WallpaperResponseOutput Result = new WallpaperResponseOutput
            {
                Result = new List<WallpaperResult>()
            };

            IHttpMultiClient.HttpMulti.InitWebProxy((Input.Proxy ?? new WallpaperProxy()).ToMapper<MultiProxy>())
                 .AddNode(opt => opt.NodePath = string.Format(All, Input.Init.Page, Input.Init.Limit))
                 .Build().RunString().FirstOrDefault().ToModel<List<JObject>>()
                 .ForEach(Item =>
                 {
                     WallpaperResult wallpaper = new WallpaperResult
                     {
                         Author = Item["author"].ToString(),
                         Created = SyncStatic.ConvertStamptime(Item["created_at"].ToString()),
                         FileSizeJepg = $"{Math.Round(Item["jpeg_file_size"].ToObject<long>() / (1024d * 1024d), 2, MidpointRounding.AwayFromZero)}MB",
                         FileSizePng = $"{Math.Round(Item["file_size"].ToObject<long>() / (1024d * 1024d), 2, MidpointRounding.AwayFromZero)}MB",
                         Height = Item["height"].ToObject<int>(),
                         Width = Item["width"].ToObject<int>(),
                         Labels = Item["tags"].ToString().Split(" ").ToList(),
                         OriginalJepg = HttpUtility.UrlDecode(Item["jpeg_url"].ToString()),
                         OriginalPng = HttpUtility.UrlDecode(Item["file_url"].ToString()),
                         Preview = HttpUtility.UrlDecode(Item["preview_url"].ToString()),
                         Rating = Item["rating"].ToString().ToUpper()
                     };
                     Result.Result.Add(wallpaper);
                 });

            return Result;
        }

        public WallpaperResponseOutput WallpaperSearch(WallpaperRequestInput Input)
        {
            WallpaperResponseOutput Result = new WallpaperResponseOutput
            {
                Result = new List<WallpaperResult>()
            };

            IHttpMultiClient.HttpMulti.InitWebProxy((Input.Proxy ?? new WallpaperProxy()).ToMapper<MultiProxy>())
                 .AddNode(opt => opt.NodePath = string.Format(Search, Input.Search.Page, Input.Search.Limit, Input.Search.KeyWord))
                 .Build().RunString().FirstOrDefault().ToModel<List<JObject>>()
                 .ForEach(Item =>
                 {
                     WallpaperResult wallpaper = new WallpaperResult
                     {
                         Author = Item["author"].ToString(),
                         Created = SyncStatic.ConvertStamptime(Item["created_at"].ToString()),
                         FileSizeJepg = $"{Math.Round(Item["jpeg_file_size"].ToObject<long>() / (1024d * 1024d), 2, MidpointRounding.AwayFromZero)}MB",
                         FileSizePng = $"{Math.Round(Item["file_size"].ToObject<long>() / (1024d * 1024d), 2, MidpointRounding.AwayFromZero)}MB",
                         Height = Item["height"].ToObject<int>(),
                         Width = Item["width"].ToObject<int>(),
                         Labels = Item["tags"].ToString().Split(" ").ToList(),
                         OriginalJepg = HttpUtility.UrlDecode(Item["jpeg_url"].ToString()),
                         OriginalPng = HttpUtility.UrlDecode(Item["file_url"].ToString()),
                         Preview = HttpUtility.UrlDecode(Item["preview_url"].ToString()),
                         Rating = Item["rating"].ToString().ToUpper()
                     };
                     Result.Result.Add(wallpaper);
                 });

            return Result;
        }
    }
}
