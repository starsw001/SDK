using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.HttpFramework.MultiOption;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Music.SDK.Basic.Impl
{
    internal class BiliBiliMusic : BasicMusic
    {
        //search_type  = music (音乐) | menus (歌单) | musician (音乐家)
        private const string SongURL = "https://api.bilibili.com/audio/music-service-c/s?keyword={0}&page={1}&pagesize=10&search_type=music";
        private const string SongSheetURL = "https://api.bilibili.com/audio/music-service-c/s?keyword={0}&page={1}&pagesize=10&search_type=menus";
        private const string PlayListURL = "https://api.bilibili.com/audio/music-service-c/menus/{0}";
        private const string PlayURL = "https://www.bilibili.com/audio/music-service-c/web/url?sid={0}";
        private const string LyricURL = "https://www.bilibili.com/audio/music-service-c/web/song/info?sid={0}";


        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<MultiProxy>())
                .AddNode(opt => opt.NodePath = string.Format(SongURL, Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = 10 * (int)jobject["data"]["num_pages"];
            foreach (var jToken in jobject["data"]["result"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = jToken["id"].ToString(),
                    SongName = jToken["title"].ToString().Replace("<em class=\"keyword\">", "").Replace("</em>", "")
                };
                SongItem.SongArtistName.Add((string)jToken["up_name"]);
                Result.SongItems.Add(SongItem);
            }

            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                SongSheetItems = new List<MusicSongSheetItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<MultiProxy>())
                .AddNode(opt => opt.NodePath = string.Format(SongSheetURL, Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = 10 * (int)jobject["data"]["num_pages"];

            foreach (var jToken in jobject["data"]["result"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                    Cover = (string)jToken["cover"],
                    ListenNumber = (string)jToken["play_count"],
                    SongSheetId = (long)jToken["id"],
                    SongSheetName = jToken["title"].ToString().Replace("<em class=\"keyword\">", "").Replace("</em>", "")
                };
                Result.SongSheetItems.Add(SongSheetItem);
            }
            return Result;
        }

        internal override MusicSongSheetDetailResult SongSheetDetail(MusicSheetSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetDetailResult Result = new MusicSongSheetDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti
               .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<MultiProxy>())
               .AddNode(opt => opt.NodePath = string.Format(PlayListURL, Input.Id))
               .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            var Info = jobject["data"]["menusRespones"];
            Result.ListenNum = (string)Info["playNum"];
            Result.MusicNum = (int)Info["songNum"];
            Result.CreateTime = SyncStatic.ConvertStamptime((((long)Info["ctime"]) / 1000).ToString()).ToFmtDate(-1, "yyyy-MM-dd");
            Result.Logo = (string)Info["coverUrl"];
            Result.DissName = (string)Info["title"];
            foreach (var jToken in jobject["data"]["songsList"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = jToken["id"].ToString(),
                    SongName = (string)jToken["title"]
                };
                SongItem.SongArtistName.Add((string)jToken["up_name"]);
                Result.SongItems.Add(SongItem);
            }
            return Result;
        }

        internal override MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input, MusicProxy Proxy)
        {
            MusicSongAlbumDetailResult Result = new MusicSongAlbumDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti
               .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<MultiProxy>())
               .AddNode(opt => opt.NodePath = string.Format(PlayListURL, Input.AlbumId))
               .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            var Info = jobject["data"]["menusRespones"];
            Result.AlbumName = (string)Info["title"];
            foreach (var jToken in jobject["data"]["songsList"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = jToken["id"].ToString(),
                    SongName = (string)jToken["title"]
                };
                SongItem.SongArtistName.Add((string)jToken["up_name"]);
                Result.SongItems.Add(SongItem);
            }
            return Result;
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input, MusicProxy Proxy)
        {
            MusicSongPlayAddressResult Result = new MusicSongPlayAddressResult
            {
                MusicPlatformType = MusicPlatformEnum.BiliBiliMusic
            };

            var response = IHttpMultiClient.HttpMulti
              .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<MultiProxy>())
              .AddNode(opt => opt.NodePath = (string)string.Format(PlayURL, Input.Dynamic))
              .Build(opt => opt.UseZip = true)
              .RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.CanPlay = !jobject["data"]["cdns"][0].ToString().IsNullOrEmpty();
            Result.SongURL = jobject["data"]["cdns"][0].ToString();

            if (Result.CanPlay)
            {
                //20*1024*1024=20971520 分段大小 20M 一首歌应该不会超过20M 192kbps
                Dictionary<string, string> Header = new Dictionary<string, string> {
                    {HeaderOption.UserAgent, "Mozilla/5.0"},
                    {HeaderOption.Referer, "https://www.bilibili.com"},
                    {"Range", "bytes=0-20971520"}
                };
                Result.BilibiliFileBytes = IHttpMultiClient.HttpMulti
                    .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<MultiProxy>())
                    .AddHeader(opt => opt.Headers = Header)
                    .AddNode(opt => opt.NodePath = Result.SongURL)
                    .Build().RunBytes().FirstOrDefault();
            }
            return Result;
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            var response = IHttpMultiClient.HttpMulti
             .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<MultiProxy>())
             .AddNode(opt => opt.NodePath = string.Format(LyricURL, Input.Dynamic))
             .Build(opt => opt.UseZip = true)
             .RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            var LyricUrl = jobject["data"]["lyric"].ToString();
            if (LyricUrl.IsNullOrEmpty())
                return new MusicLyricResult();
            var Lyr = new WebClient().DownloadString(LyricUrl);
            MusicLyricResult Result = new MusicLyricResult
            {
                BiliBiliLyric = Lyr
            };
            if (Result.Title.IsNullOrEmpty())
                Result.Title = (string)jobject["data"]["title"];
            if (Result.Artist.IsNullOrEmpty())
                Result.Artist = (string)jobject["data"]["author"];
            return Result;
        }
    }
}
