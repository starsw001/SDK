using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongItemResult
    {
        public int Total { get; set; }
        public List<MusicSongItem> SongItems { get; set; }
    }
}
