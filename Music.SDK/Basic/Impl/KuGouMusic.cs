using Music.SDK.Utilily.KuGouUtility;
using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XExten.Advance.CacheFramework;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;

namespace Music.SDK.Basic.Impl
{
    internal class KuGouMusic : BasicMusic
    {
        private const string SongURL = "https://songsearch.kugou.com/song_search_v2?keyword={0}&page={1}&pagesize=10";
        private const string SongSheetURL = "https://complexsearch.kugou.com/v1/search/special";
        private const string PlayListURL = "http://mobilecdnbj.kugou.com/api/v3/special/song?specialid={0}&page={1}&pagesize=10";
        private const string AlbumURL = "http://mobilecdn.kugou.com/api/v3/album/song?albumid={0}&pagesize=9999&page=1";
        private const string PlayURL = "https://wwwapi.kugou.com/yy/index.php?r=play/getdata&hash={0}";

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
            var jobject = response[12..^2].ToModel<JObject>();
            Result.Total = (int)jobject["data"]["total"];
            foreach (var jToken in jobject["data"]["lists"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                    Cover = (string)jToken["img"],
                    SongSheetId = (long)jToken["specialid"],
                    SongSheetName = (string)jToken["specialname"],
                    CreateTime = ((DateTime)jToken["publish_time"]).ToString("yyyy-MM-dd"),
                    ListenNumber = (string)jToken["play_count"],
                    MusicPlatformType = MusicPlatformEnum.KuGouMusic
                };
                Result.SongSheetItems.Add(SongSheetItem);
            }
            return Result;
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetDetailResult Result = new MusicSongSheetDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .AddNode(string.Format(PlayListURL, Input.Id, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            Result.MusicNum = (int)jobject["data"]["total"];
            foreach (var jToken in jobject["data"]["info"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongFileHash = (string)jToken["hash"],
                    SongName = ((string)jToken["filename"]).Split("-").LastOrDefault().Trim(),
                    SongAlbumId = (long)jToken["album_id"]
                };
                SongItem.SongArtistName.Add(((string)jToken["filename"]).Split("-").FirstOrDefault().Trim());
                Result.SongItems.Add(SongItem);
            }

            return Result;
        }

        internal override MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input, MusicProxy Proxy)
        {
            MusicSongAlbumDetailResult Result = new MusicSongAlbumDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                SongItems = new List<MusicSongItem>()
            };
            if (Input.AlbumId.Equals("0")) return Result;
            var response = IHttpMultiClient.HttpMulti
              .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
              .AddNode(string.Format(AlbumURL, Input.AlbumId))
              .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            foreach (var jToken in jobject["data"]["info"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongFileHash = (string)jToken["hash"],
                    SongName = ((string)jToken["filename"]).Split("-").LastOrDefault().Trim(),
                    SongAlbumId = (long)jToken["album_id"]
                };
                var ArtistName = jToken["filename"].ToString().Split("-").FirstOrDefault().Trim().Split(new string[] { "、" }, StringSplitOptions.None);
                SongItem.SongArtistName.AddRange(ArtistName);
                Result.SongItems.Add(SongItem);
            }

            return Result;
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input, MusicProxy Proxy)
        {
            MusicSongPlayAddressResult Result = new MusicSongPlayAddressResult
            {
                MusicPlatformType = MusicPlatformEnum.KuGouMusic
            };
            var URL = $"{string.Format(PlayURL, Input.Dynamic)}{(Input.KuGouAlbumId == 0 ? "" : $"&album_id={Input.KuGouAlbumId}")}";
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header("Cookie", "kg_mid=c4ca4238a0b923820dcc509a6f75849b")
                .Header("Referer", "https://www.kugou.com/")
                .Header("Origin", "https://www.kugou.com/")
                .AddNode(URL)
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.CanPlay = !jobject["data"]["play_url"].ToString().IsNullOrEmpty();
            Result.SongURL = jobject["data"]["play_url"].ToString();

            // 歌词
            var Lyric = new MusicLyricResult(Regex.Unescape(jobject["data"]["lyrics"].ToString()));
            if (Caches.RunTimeCacheGet<MusicLyricResult>(Input.Dynamic) == null)
                Caches.RunTimeCacheSet<MusicLyricResult>(Input.Dynamic, Lyric, 10);
            return Result;
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            var Result = Caches.RunTimeCacheGet<MusicLyricResult>(Input.Dynamic);
            if (Result == null)
                return new MusicLyricResult { Title = "请先调用获取播放地址" };
            else
                return Result;
        }
    }
}
