using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel
{
    public class MusicRequestInput
    {
        /// <summary>
        /// 音乐平台
        /// </summary>
        public MusicPlatformEnum MusicPlatformType { get; set; }
        /// <summary>
        /// 关键字检索
        /// </summary>
        public MusicSearch Search { get; set; }
        /// <summary>
        /// 地址检索
        /// </summary>
        public MusicPlaySearch PlaySearch { get; set; }
        /// <summary>
        /// 歌词检索
        /// </summary>
        public MusicLyricSearch LyricSearch { get; set; }
    }
}
