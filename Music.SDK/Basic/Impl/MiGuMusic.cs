using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;

namespace Music.SDK.Basic.Impl
{
    internal class MiGuMusic : BasicMusic
    {
        private const string SongURL = "https://m.music.migu.cn/migu/remoting/scr_search_tag?rows=10&type=song&keyword={0}&pgc={1}";
        private const string SongSheetURL = "";
        private const string PlayListURL = "";
        private const string AlbumURL = "";
        private const string PlayURL = "";
        private const string LyricURL = "";
        private Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            {"Referer","http://m.music.migu.cn" },
        };

        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                MusicPlatformType = MusicPlatformEnum.MiGuMusic,
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
              .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
              .Header(Headers)
              .AddNode(string.Format(SongURL, Input.KeyWord, Input.Page))
              .Build().RunString().FirstOrDefault();

            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }
    }
}
