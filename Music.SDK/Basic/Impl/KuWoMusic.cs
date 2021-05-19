using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;

namespace Music.SDK.Basic.Impl
{
    internal class KuWoMusic : BasicMusic
    {
        private const string Referer = "Referer";
        private const string Host = "http://www.kuwo.cn";
        private const string SongURL = "http://www.kuwo.cn/api/www/search/{0}?key={1}&pn={2}&rn=10";
        private const string PlayListURL = "http://www.kuwo.cn/api/www/playlist/playListInfo?pid={0}&pn={1}&rn=10";
        private const string AlbumURL = "http://www.kuwo.cn/api/www/album/albumInfo?albumId={0}&pn=1&rn=100";
        private const string LyricURL = "http://m.kuwo.cn/newh5/singles/songinfoandlrc?musicId={0}";
        private const string PlayURL = "http://www.kuwo.cn/url?format=mp3&rid={0}&response=url&type=convert_url3&br=320kmp3&from=web";
        private Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            { "Cookie","_ga=GA1.2.1583975401.1620899486; _gid=GA1.2.928567518.1620899486; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1620899486,1620956477; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1620956477; _gat=1; kw_token=TSGD1QV8KJA"},
            { "csrf","TSGD1QV8KJA"}
        };

        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Referer, Host + $"/search/playlist?key={HttpUtility.UrlEncode(Input.KeyWord)}")
                .Header(Headers)
                .AddNode(string.Format(SongURL, "searchMusicBykeyWord", Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            Result.Total = (int)jobject["data"]["total"];

            foreach (var jToken in jobject["data"]["list"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumName = (string)jToken["album"],
                    SongId = (long)jToken["rid"],
                    SongName = (string)jToken["name"],
                };
                SongItem.SongArtistId.Add((long)jToken["artistid"]);
                SongItem.SongArtistName.Add((string)jToken["artist"]);
                Result.SongItems.Add(SongItem);
            }

            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                SongSheetItems = new List<MusicSongSheetItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Referer, Host + $"/search/playlist?key={HttpUtility.UrlEncode(Input.KeyWord)}")
                .Header(Headers)
                .AddNode(string.Format(SongURL, "searchPlayListBykeyWord", Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();
            var jobject = response.ToModel<JObject>();

            Result.Total = (int)jobject["data"]["total"];
            foreach (var jToken in jobject["data"]["list"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                    Cover = (string)jToken["img"],
                    ListenNumber = (string)jToken["listencnt"],
                    SongSheetId = (long)jToken["id"],
                    SongSheetName = (string)jToken["name"]
                };
                Result.SongSheetItems.Add(SongSheetItem);
            }
            return Result;
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetDetailResult Result = new MusicSongSheetDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers).AddNode(string.Format(PlayListURL, Input.Id, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.MusicNum = (int)jobject["data"]["total"];
            Result.DissName = (string)jobject["data"]["name"];
            Result.Logo = (string)jobject["data"]["img"];
            Result.ListenNum = (string)jobject["data"]["listencnt"];

            foreach (var jToken in jobject["data"]["musicList"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumName = (string)jToken["album"],
                    SongId = (long)jToken["rid"],
                    SongName = (string)jToken["name"],
                };
                SongItem.SongArtistId.Add((long)jToken["artistid"]);
                SongItem.SongArtistName.Add((string)jToken["artist"]);
                Result.SongItems.Add(SongItem);
            }
            return Result;
        }

        internal override MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input, MusicProxy Proxy)
        {
            MusicSongAlbumDetailResult Result = new MusicSongAlbumDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers).AddNode(string.Format(AlbumURL, Input.AlbumId))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            Result.AlbumName = (string)jobject["data"]["album"];

            foreach (var jToken in jobject["data"]["musicList"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumName = (string)jToken["album"],
                    SongId = (long)jToken["rid"],
                    SongName = (string)jToken["name"],
                };
                SongItem.SongArtistId.Add((long)jToken["artistid"]);
                SongItem.SongArtistName.Add((string)jToken["artist"]);
                Result.SongItems.Add(SongItem);
            }
            return Result;
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input, MusicProxy Proxy)
        {
            MusicSongPlayAddressResult Result = new MusicSongPlayAddressResult
            {
                MusicPlatformType = MusicPlatformEnum.KuWoMusic
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>()).Header(Headers)
                .AddNode((string)string.Format(PlayURL, Input.Dynamic))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.CanPlay = !jobject["url"].AsString().IsNullOrEmpty();
            Result.SongURL = (string)jobject["url"];
            return Result;
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            MusicLyricResult Result = new MusicLyricResult();

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>()).Header(Headers)
                .AddNode((string)string.Format(LyricURL, Input.Dynamic)).Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Title = (string)jobject["data"]["songinfo"]["songName"];
            Result.Artist = (string)jobject["data"]["songinfo"]["artist"];
            Result.Album = (string)jobject["data"]["songinfo"]["album"];
            Result.Offset = "0";

            foreach (var jToken in jobject["data"]["lrclist"])
            {
                MusicLyricItemResult lineLyricItem = new MusicLyricItemResult
                {
                    Lyric = (string)jToken["lineLyric"],
                    Time = TimeSpan.FromSeconds(double.Parse((string)jToken["time"])).ToString(@"mm\:ss\.ff")
                };
                Result.Lyrics.Add(lineLyricItem);
            }

            return Result;
        }
    }
}
