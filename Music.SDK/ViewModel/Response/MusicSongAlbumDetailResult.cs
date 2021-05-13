using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongAlbumDetailResult
    {
        /// <summary>
        /// 专辑名称
        /// </summary>
        public string AlbumName { get; set; }
        /// <summary>
        /// 歌曲信息
        /// </summary>
        public List<MusicSongItem> SongItems { get; set; }
    }
}
