using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.Basic.Impl
{
    internal class KuGouMusic : BasicMusic
    {
        internal override MusicSongItemResult SearchSong(string KeyWord, int Page)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongSheetResult SearchSongSheet(string KeyWord, int Page)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(string SheetId)
        {
            throw new NotImplementedException();
        }
    }
}
