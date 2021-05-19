using Music.SDK.ViewModel.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongPlayAddressResult
    {
        /// <summary>
        /// 歌曲平台
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicPlatformEnum? MusicPlatformType { get; set; }
        /// <summary>
        /// 能否播放
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool CanPlay { get; set; }
        /// <summary>
        /// 歌曲地址 B站的连接直接访问会报403注意B站只能使用文件流
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongURL { get; set; }
        /// <summary>
        /// B站文件流
        /// </summary>
        [JsonIgnore]
        public byte[] BilibiliFileBytes { get; set; }
    }
}
