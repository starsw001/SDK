using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Music.SDK.Basic.Impl
{
    internal class NeteaseMusic : BasicMusic
    {
        private const string SongURL = "https://music.163.com/api/cloudsearch/pc?s={0}&offset={1}&limit=10&type={2}";
        private const string SongDetailURL = "https://music.163.com/api/v3/song/detail";
        private const string PlayListURL = "https://music.163.com/api/v6/playlist/detail?s=8&n=100000&id={0}";
        private const string AlbumURL = "";
        private const string PlayURL = "";
        private const string LyricURL = "";


        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                MusicPlatformType = MusicPlatformEnum.NeteaseMusic,
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .AddNode(string.Format(SongURL, Input.KeyWord, Input.Page, 1), RequestType.POST)
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = (int)jobject["result"]["songCount"];
            foreach (var jToken in jobject["result"]["songs"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongName = jToken["name"].ToString(),
                    SongId = (long)jToken["id"],
                    SongAlbumId = (long)jToken["al"]["id"],
                    SongAlbumName = (string)jToken["al"]["name"]
                };
                foreach (var artist in jToken["ar"])
                {
                    SongItem.SongArtistId.Add((long)artist["id"]);
                    SongItem.SongArtistName.Add((string)artist["name"]);
                }
                Result.SongItems.Add(SongItem);
            }

            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                MusicPlatformType = MusicPlatformEnum.NeteaseMusic,
                SongSheetItems = new List<MusicSongSheetItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .AddNode(string.Format(SongURL, Input.KeyWord, Input.Page, 1000), RequestType.POST)
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = (int)jobject["result"]["playlistCount"];
            foreach (var jToken in jobject["result"]["playlists"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                    Cover = jToken["coverImgUrl"].ToString(),
                    ListenNumber = jToken["playCount"].ToString(),
                    SongSheetId = (long)jToken["id"],
                    SongSheetName = jToken["name"].ToString()
                };
                Result.SongSheetItems.Add(SongSheetItem);
            }
            return Result;
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetDetailResult Result = new MusicSongSheetDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.NeteaseMusic,
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
               .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
               .AddNode(string.Format(PlayListURL, Input.Id), RequestType.POST)
               .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            var dateTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);
            TimeSpan value = new TimeSpan((jobject["playlist"]["createTime"].ToString() + "0000").AsLong());
            Result.CreateTime = dateTime.Add(value).ToFmtDate(-1, "yyyy-MM-dd");
            Result.Logo = jobject["playlist"]["coverImgUrl"].ToString();
            Result.DissName = jobject["playlist"]["name"].ToString();
            Result.ListenNum = jobject["playlist"]["playCount"].ToString();
            Result.MusicNum = (int)jobject["playlist"]["trackCount"];
            List<long> SongIds = new List<long>();
            foreach (var jToken in jobject["playlist"]["trackIds"])
            {
                SongIds.Add((long)jToken["id"]);
            }
            var Value = $"[{string.Join(",", SongIds.Select(t => "{\"id\":" + t + "}"))}]";

            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>
            {
                 new KeyValuePair<string, string>("c",Value)
            };
            var res = IHttpMultiClient.HttpMulti
                 .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                 .AddNode(SongDetailURL, data, RequestType.POST)
                 .Build().RunString().FirstOrDefault();

            return Result;
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
    }
}
