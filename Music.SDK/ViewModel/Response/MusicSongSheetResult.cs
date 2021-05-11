using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel.Response
{
    public class MusicSongSheetResult
    {
        public int Total { get; set; }
        public List<MusicSongSheetItem> SongSheetItems { get; set; }
    }
}
