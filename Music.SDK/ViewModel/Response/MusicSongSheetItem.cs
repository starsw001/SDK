using Music.SDK.ViewModel.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongSheetItem
    {
        /// <summary>
        /// 歌单Id
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long SongSheetId { get; set; }
        /// <summary>
        /// 歌单名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongSheetName { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Cover { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CreateTime { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ListenNumber { get; set; }
    }
}
