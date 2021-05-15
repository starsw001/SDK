using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK.Basic.Impl
{
    internal class QianQianMusic : BasicMusic
    {
        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }
    }
}
