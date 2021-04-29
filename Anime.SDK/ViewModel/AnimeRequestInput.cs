using Anime.SDK.ViewModel.Enums;
using Anime.SDK.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel
{
    public class AnimeRequestInput
    {
        /// <summary>
        /// 模式
        /// </summary>
        public AnimeEnum AnimeType { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        public AnimeProxy Proxy { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        public AnimeInit Init { get; }
        /// <summary>
        /// 搜索
        /// </summary>
        public AnimeSearch Search { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public AnimeCategory Category { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public AnimeDetail Detail { get; set; }
        /// <summary>
        /// 播放
        /// </summary>
        public AnimeWatchPlay WatchPlay { get; set; }
    }
}
