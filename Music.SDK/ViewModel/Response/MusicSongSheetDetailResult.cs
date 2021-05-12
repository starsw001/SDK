using Music.SDK.ViewModel.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongSheetDetailResult
    {
        /// <summary>
        /// 歌曲平台
        /// </summary>
        public MusicPlatformEnum? MusicPlatformType { get; set; }
        /// <summary>
        /// 歌单名称
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DissName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Logo { get; set; }
        /// <summary>
        /// 播放次数
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ListenNum { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CreateTime { get; set; }
        /// <summary>
        /// 曲目数量
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int MusicNum { get; set; }
        /// <summary>
        /// 歌曲信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MusicSongItem> SongItems { get; set; }
    }
}
