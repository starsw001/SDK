using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;

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

        internal override MusicSongAlbumDetailResult SongAlbumDetail(string AlbumId)
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
