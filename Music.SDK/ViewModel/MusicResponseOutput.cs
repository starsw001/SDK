using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.ViewModel
{
    public class MusicResponseOutput
    {
        public MusicSongItemResult SongItemResult { get; set; }
        public MusicSongSheetResult SongSheetResult { get; set; }
    }
}
