using Music.SDK.ViewModel.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongSheetResult
    {
        /// <summary>
        /// 歌曲平台
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicPlatformEnum? MusicPlatformType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MusicSongSheetItem> SongSheetItems { get; set; }
    }
}
