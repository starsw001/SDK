using Music.SDK.Utilily.KuGouUtility;
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

namespace Music.SDK.Basic.Impl
{
    internal class KuGouMusic : BasicMusic
    {
        private const string SongURL = "https://songsearch.kugou.com/song_search_v2?keyword={0}&page={1}&pagesize=10";
        private const string SongSheetURL = "https://complexsearch.kugou.com/v1/search/special";
        private const string PlayListURL = "http://m.kugou.com/plist/list/{0}?json=true";
        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .AddNode(string.Format(SongURL, Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = (int)jobject["data"]["total"];
            foreach (var jToken in jobject["data"]["lists"])
            {
                string fileHash = (string)jToken["FileHash"];
                long albumId = ((string)jToken["AlbumID"]).AsLong();
                MusicSongItem songItem = new MusicSongItem
                {
                    MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                    SongFileHash = fileHash,
                    SongName = (string)jToken["SongName"],
                    SongAlbumId = albumId,
                    SongAlbumName = (string)jToken["AlbumName"]
                };
                songItem.SongArtistName.AddRange(((string)jToken["SingerName"]).Split(new string[] { "、" }, StringSplitOptions.None));
                foreach (var id in jToken["SingerId"])
                {
                    songItem.SongArtistId.Add((long)id);
                }
                Result.SongItems.Add(songItem);
            }
            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                SongSheetItems = new List<MusicSongSheetItem>()
            };
            var Host = SongSheetURL + KuGouHelper.GetParam(Input.KeyWord, Input.Page);
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .AddNode(Host)
                .Build().RunString().FirstOrDefault();
            var jobject = response.Substring(12, response.Length - 14).ToModel<JObject>();
            Result.Total = (int)jobject["data"]["total"];
            foreach (var jToken in jobject["data"]["lists"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                     Cover= (string)jToken["img"],
                     SongSheetId=(long)jToken["specialid"],
                     SongSheetName=(string)jToken["specialname"],
                     CreateTime=((DateTime)jToken["publish_time"]).ToString("yyyy-MM-dd"),
                     ListenNumber=(string)jToken["play_count"],
                     MusicPlatformType=MusicPlatformEnum.KuGouMusic
                };
                Result.SongSheetItems.Add(SongSheetItem);
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
                .AddNode(string.Format(PlayListURL,Input.Id))
                .Build().RunString().FirstOrDefault();

            return Result;
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
