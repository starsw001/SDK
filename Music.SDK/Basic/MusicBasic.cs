using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.Basic
{
    internal abstract class BasicMusic
    {
        /// <summary>
        /// 歌曲检索
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        internal abstract MusicSongItemResult SearchSong(string KeyWord, int Page);
        /// <summary>
        /// 歌单检索
        /// </summary>
        /// <param name="KeyWord"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        internal abstract MusicSongSheetResult SearchSongSheet(string KeyWord, int Page);
    }
}
