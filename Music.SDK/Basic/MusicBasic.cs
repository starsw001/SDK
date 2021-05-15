using Music.SDK.ViewModel.Request;
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
        /// <param name="Input"></param>
        /// <param name="Proxy"></param>
        /// <returns></returns>
        internal abstract MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy);
        /// <summary>
        /// 歌单检索
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="Proxy"></param>
        /// <returns></returns>
        internal abstract MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy);
        /// <summary>
        /// 获取歌单详情
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="Proxy"></param>
        /// <returns></returns>
        internal abstract MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input,MusicProxy Proxy);
        /// <summary>
        /// 获取关联专辑
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="Proxy"></param>
        /// <returns></returns>
        internal abstract MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input,MusicProxy Proxy);
        /// <summary>
        /// 获取播放地址
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="Proxy"></param>
        /// <returns></returns>
        internal abstract MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input,MusicProxy Proxy);
        /// <summary>
        /// 获取歌词
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="Proxy"></param>
        /// <returns></returns>
        internal abstract MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy);
    }
}
