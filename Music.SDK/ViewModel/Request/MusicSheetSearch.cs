using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Request
{
    public class MusicSheetSearch
    {
        /// <summary>
        /// 歌单Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 酷狗 酷我 咪咕 可用
        /// </summary>
        public int Page { get; set; } = 1;
    }
}
