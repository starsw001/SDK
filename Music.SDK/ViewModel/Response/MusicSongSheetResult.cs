using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongSheetResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MusicSongSheetItem> SongSheetItems { get; set; }
    }
}
