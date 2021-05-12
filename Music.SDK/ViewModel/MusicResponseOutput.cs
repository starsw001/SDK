using Music.SDK.ViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel
{
    public class MusicResponseOutput
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongItemResult SongItemResult { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongSheetResult SongSheetResult { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongSheetDetailResult SongSheetDetailResult { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicSongPlayAddressResult SongPlayAddressResult { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MusicLyricResult SongLyricResult { get; set; }
    }
}
