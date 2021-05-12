using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.Basic.Impl
{
    internal class KuWoMusic : BasicMusic
    {
        internal override MusicSongItemResult SearchSong(string KeyWord, int Page)
        {
            throw new NotImplementedException();
        }
        internal override MusicSongSheetResult SearchSongSheet(string KeyWord, int Page)
        {
            throw new NotImplementedException();
        }

        internal override MusicLyricResult SongLyric(dynamic Dynamic)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(dynamic Dynamic)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(string SheetId)
        {
            throw new NotImplementedException();
        }
    }
}
