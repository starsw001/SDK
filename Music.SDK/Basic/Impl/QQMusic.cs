using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Music.SDK.Basic.Impl
{
    internal class QQMusic : BasicMusic
    {
        private const string SongURL = "https://c.y.qq.com/soso/fcgi-bin/client_search_cp?w={0}&p={1}&n=10&format=json";
        private const string SongSheetURL = "https://c.y.qq.com/soso/fcgi-bin/client_music_search_songlist?remoteplace=txt.yqq.playlist&query={0}&page_no={1}&num_per_page=10&format=json";
        private const string PlayListURL = "https://c.y.qq.com/qzone/fcg-bin/fcg_ucc_getcdinfo_byids_cp.fcg?type=1&onlysong=0&disstid={0}&format=json&outCharset=utf-8";
        private const string AlbumURL = "https://c.y.qq.com/v8/fcg-bin/musicmall.fcg?format=json&outCharset=utf-8&albummid={0}&cmd=get_album_buy_page";
        private const string PlayURL = "https://u.y.qq.com/cgi-bin/musicu.fcg?data={\"req_0\":{\"module\":\"vkey.GetVkeyServer\",\"method\":\"CgiGetVkey\",\"param\":{\"guid\":\"1535153710\",\"songmid\":[\"@id\"],\"songtype\":[0],\"uin\":\"0\",\"loginflag\":1,\"platform\":\"20\"}},\"comm\":{\"uin\":0,\"format\":\"json\",\"ct\":24,\"cv\":0}}";
        private const string LyricURL = "https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric_new.fcg?songmid={0}&format=json&nobase64=1";
        private Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            {"Referer","https://y.qq.com" },
            {"Origin","https://y.qq.com" }
        };

        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers)
                .AddNode(string.Format(SongURL, Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = jobject.SelectToken("data.song.totalnum").ToString().AsInt();
            foreach (var jToken in jobject["data"]["song"]["list"])
            {
                MusicSongItem Item = new MusicSongItem
                {
                    MusicPlatformType = MusicPlatformEnum.QQMusic,
                    SongId = (long)jToken["songid"],
                    SongMId = (string)jToken["songmid"],
                    SongName = (string)jToken["songname"],
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumMId = (string)jToken["albummid"],
                    SongAlbumName = (string)jToken["albumname"]
                };
                foreach (var artist in jToken["singer"])
                {
                    Item.SongArtistId.Add((long)artist["id"]);
                    Item.SongArtistMId.Add((string)artist["mid"]);
                    Item.SongArtistName.Add((string)artist["name"]);
                }
                Result.SongItems.Add(Item);
            }
            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                SongSheetItems = new List<MusicSongSheetItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers)
                .AddNode(string.Format(SongSheetURL, Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = jobject.SelectToken("data.sum").ToString().AsInt();
            foreach (var jToken in jobject["data"]["list"])
            {
                MusicSongSheetItem Item = new MusicSongSheetItem
                {
                    MusicPlatformType = MusicPlatformEnum.QQMusic,
                    Cover = (string)jToken["imgurl"],
                    SongSheetId = (long)jToken["dissid"],
                    SongSheetName = (string)jToken["dissname"],
                    CreateTime = (string)jToken["createtime"],
                    ListenNumber = (string)jToken["listennum"]
                };
                Result.SongSheetItems.Add(Item);
            }
            return Result;
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetDetailResult Result = new MusicSongSheetDetailResult
            {
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers)
                .AddNode(string.Format(PlayListURL, Input.Id))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.MusicNum = (int)jobject["cdlist"][0]["total_song_num"];
            Result.CreateTime = SyncStatic.ConvertStamptime((string)jobject["cdlist"][0]["ctime"]).ToString("yyyy-MM-dd");
            Result.DissName = (string)jobject["cdlist"][0]["dissname"];
            Result.ListenNum = (string)jobject["cdlist"][0]["visitnum"];
            Result.Logo = (string)jobject["cdlist"][0]["logo"];
            Result.MusicPlatformType = MusicPlatformEnum.QQMusic;
            foreach (var jToken in jobject["cdlist"][0]["songlist"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = (long)jToken["songid"],
                    SongMId = (string)jToken["songmid"],
                    SongName = (string)jToken["songname"],
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumMId = (string)jToken["albummid"],
                    SongAlbumName = (string)jToken["albumname"]
                };
                foreach (var artist in jToken["singer"])
                {
                    SongItem.SongArtistId.Add((long)artist["id"]);
                    SongItem.SongArtistMId.Add((string)artist["mid"]);
                    SongItem.SongArtistName.Add((string)artist["name"]);
                }
                Result.SongItems.Add(SongItem);
            }
            return Result;
        }

        internal override MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input, MusicProxy Proxy)
        {
            MusicSongAlbumDetailResult Result = new MusicSongAlbumDetailResult
            {
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers)
                .AddNode(string.Format(AlbumURL, Input.AlbumId))
                .Build().RunString().FirstOrDefault();
            var jobject = response.ToModel<JObject>();
            Result.AlbumName = (string)jobject.SelectToken("data.album_name");
            Result.MusicPlatformType = MusicPlatformEnum.QQMusic;
            foreach (var jToken in jobject["data"]["songlist"])
            {
                MusicSongItem Item = new MusicSongItem
                {
                    MusicPlatformType = MusicPlatformEnum.QQMusic,
                    SongId = (long)jToken["songid"],
                    SongMId = (string)jToken["songmid"],
                    SongName = (string)jToken["songname"],
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumMId = (string)jToken["albummid"],
                    SongAlbumName = (string)jToken["albumname"]
                };
                foreach (var artist in jToken["singer"])
                {
                    Item.SongArtistId.Add((long)artist["id"]);
                    Item.SongArtistMId.Add((string)artist["mid"]);
                    Item.SongArtistName.Add((string)artist["name"]);
                }
                Result.SongItems.Add(Item);
            }
            return Result;
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input, MusicProxy Proxy)
        {
            MusicSongPlayAddressResult Result = new MusicSongPlayAddressResult
            {
                MusicPlatformType = MusicPlatformEnum.QQMusic
            };

            string Host = PlayURL.Replace("@id", Input.Dynamic);
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .AddNode(Host).Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            var SongIp = (string)jobject["req_0"]["data"]["sip"][0];
            var PlayUrl = (string)jobject["req_0"]["data"]["midurlinfo"][0]["purl"];

            Result.CanPlay = !PlayUrl.IsNullOrEmpty();
            Result.SongURL = SongIp + PlayUrl;
            return Result;
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers)
                .AddNode((string)string.Format(LyricURL, Input.Dynamic))
                .Build().RunString().FirstOrDefault();
            var jToken = response.ToModel<JObject>().Value<string>("lyric");
            if (!jToken.IsNullOrEmpty())
            {
                return new MusicLyricResult(jToken.ToString());
            }
            return null;
        }

    }
}
