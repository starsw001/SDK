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
        /// <summary>
        /// 获取歌单详情
        /// </summary>
        /// <param name="SheetId"></param>
        /// <returns></returns>
        internal abstract MusicSongSheetDetailResult SongSheetDetail(string SheetId);
        /// <summary>
        /// 获取专辑
        /// </summary>
        /// <param name="AlbumId"></param>
        /// <returns></returns>
        internal abstract MusicSongAlbumDetailResult SongAlbumDetail(string AlbumId);
        /// <summary>
        /// 获取播放地址
        /// </summary>
        /// <param name="Dynamic"></param>
        /// <returns></returns>
        internal abstract MusicSongPlayAddressResult SongPlayAddress(dynamic Dynamic);
        /// <summary>
        /// 获取歌词
        /// </summary>
        /// <param name="Dynamic"></param>
        /// <returns></returns>
        internal abstract MusicLyricResult SongLyric(dynamic Dynamic);
    }
}
