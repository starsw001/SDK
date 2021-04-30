using Newtonsoft.Json;
using Synctool.LinqFramework;
using Synctool.StaticFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wallpaper.SDK.ViewModel.Response
{
    public class WallpaperResult
    {
        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// 预览图
        /// </summary>
        public string Preview { get; set; }
        /// <summary>
        /// 原图
        /// </summary>
        public string OriginalJepg { get; set; }
        /// <summary>
        /// 原图
        /// </summary>
        public string OriginalPng { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSizeJepg { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSizePng { get; set; }
        /// <summary>
        /// 分级
        /// </summary>
        public string Rating { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> Labels { get; set; }
        public DateTime Created { get; set; }
        public string Author { get; set; }
    }
}
