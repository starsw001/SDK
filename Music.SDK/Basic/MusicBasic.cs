using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.Basic
{
    public abstract class BasicMusic
    {
        public abstract MusicSearchResult SearchSong(string KeyWord, int Page = 0);
        public abstract MusicSongSheetResult SearchSongSheet(string KeyWord, int Page = 1);
    }
}
