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
        /// 歌曲ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongId { get; set; }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongName { get; set; }

        /// <summary>
        /// 歌曲专辑ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongAlbumId { get; set; }

        /// <summary>
        /// 歌曲专辑名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongAlbumName { get; set; }

        /// <summary>
        /// 歌曲艺术家ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SongArtistId { get; set; } = new List<string>();

        /// <summary>
        /// 歌曲艺术家名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> SongArtistName { get; set; } = new List<string>();
    }
}
