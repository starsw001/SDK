using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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
        private Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            { "Cookie","_ga=GA1.2.1583975401.1620899486; _gid=GA1.2.928567518.1620899486; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1620899486,1620956477; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1620956477; _gat=1; kw_token=TSGD1QV8KJA"},
            { "csrf","TSGD1QV8KJA"}
        };

        internal override MusicSongItemResult SearchSong(string KeyWord, int Page)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .Header(Referer, Host + $"/search/playlist?key={HttpUtility.UrlEncode(KeyWord)}")
                .Header(Headers)
                .AddNode(string.Format(SongURL, "searchMusicBykeyWord", KeyWord, Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            Result.Total = (int)jobject["data"]["total"];

            foreach (var jToken in jobject["data"]["list"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumName = (string)jToken["album"],
                    MusicPlatformType= MusicPlatformEnum.KuWoMusic,
                    SongId=(long)jToken["rid"],
                    SongName=(string)jToken["name"],
                };
                SongItem.SongArtistId.Add((long)jToken["artistid"]);
                SongItem.SongArtistName.Add((string)jToken["artist"]);
                Result.SongItems.Add(SongItem);
            }

            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(string KeyWord, int Page)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                SongSheetItems = new List<MusicSongSheetItem>()
            };

            var response = IHttpMultiClient.HttpMulti
               .Header(Referer, Host + $"/search/playlist?key={HttpUtility.UrlEncode(KeyWord)}")
               .Header(Headers)
               .AddNode(string.Format(SongURL, "searchPlayListBykeyWord", KeyWord, Page))
               .Build().RunString().FirstOrDefault();
            var jobject = response.ToModel<JObject>();

            Result.Total = (int)jobject["data"]["total"];
            foreach (var jToken in jobject["data"]["list"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                     MusicPlatformType=MusicPlatformEnum.KuWoMusic,
                     Cover= (string)jToken["img"],
                     ListenNumber= (string)jToken["listencnt"],
                     SongSheetId= (long)jToken["id"],
                     SongSheetName=(string)jToken["name"]
                };
                Result.SongSheetItems.Add(SongSheetItem);
            }   
            return Result;
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

        internal override MusicSongSheetDetailResult SongSheetDetail(string SheetId, int Page)
        {
            MusicSongAlbumDetailResult Result = new MusicSongAlbumDetailResult
            {
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti.Header(Headers)
             .AddNode(string.Format(PlayListURL, SheetId, Page))
             .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.MusicPlatformType = MusicPlatformEnum.KuWoMusic;
            Result.AlbumName = (string)jobject["data"]["name"];



            throw new NotImplementedException();
        }
    }
}
