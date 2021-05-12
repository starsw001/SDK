using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongPlayAddressResult
    {
        /// <summary>
        /// 歌曲地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SongURL { get; set; }
    }
}
