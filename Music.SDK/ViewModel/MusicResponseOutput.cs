using Music.SDK.ViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel
{
    public class MusicResponseOutput
    {
        /// <summary>
        /// 曲目结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongItemResult SongItemResult { get; set; }
        /// <summary>
        /// 歌单结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongSheetResult SongSheetResult { get; set; }
        /// <summary>
        /// 歌单详情
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongSheetDetailResult SongSheetDetailResult { get; set; }
        /// <summary>
        /// 曲目地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongPlayAddressResult SongPlayAddressResult { get; set; }
        /// <summary>
        /// 歌词结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicLyricResult SongLyricResult { get; set; }
        /// <summary>
        /// 专辑结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongAlbumDetailResult SongAlbumDetailResult { get; set; }
    }
}
