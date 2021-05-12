using Music.SDK.ViewModel.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongItem
    {
        /// <summary>
        /// 歌曲平台
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicPlatformEnum? MusicPlatformType { get; set; }

        /// <summary>
        /// 歌曲Url链接
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongUrl { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long SongId { get; set; }

        /// <summary>
        /// 歌曲MID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongMId { get; set; }

        /// <summary>
        /// 歌曲GID (通用ID)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongGId { get; set; }

        /// <summary>
        /// 歌曲文件Hash
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongFileHash { get; set; }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongName { get; set; }

        /// <summary>
        /// 歌曲艺术家ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<long> SongArtistId { get; set; } = new List<long>();

        /// <summary>
        /// 歌曲艺术家MID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SongArtistMId { get; set; } = new List<string>();

        /// <summary>
        /// 歌曲艺术家名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SongArtistName { get; set; } = new List<string>();

        /// <summary>
        /// 歌曲专辑ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long SongAlbumId { get; set; }

        /// <summary>
        /// 歌曲专辑MID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongAlbumMId { get; set; }

        /// <summary>
        /// 歌曲专辑名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongAlbumName { get; set; }
    }
}
