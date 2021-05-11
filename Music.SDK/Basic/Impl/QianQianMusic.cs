using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.Basic.Impl
{
    internal class QianQianMusic : BasicMusic
    {
        public override MusicSearchResult SearchSong(string KeyWord, int Page = 0)
        {
            throw new NotImplementedException();
        }

        public override MusicSongSheetResult SearchSongSheet(string KeyWord, int Page = 1)
        {
            throw new NotImplementedException();
        }
    }
}
