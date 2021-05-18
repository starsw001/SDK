using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
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
        private const string LyricURL = "";


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
            Result.Total = 10 * (int)jobject["data"]["num_pages"];
            foreach (var jToken in jobject["data"]["result"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                    SongId = (long)jToken["id"],
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
                SongSheetItems = new List<MusicSongSheetItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .AddNode(string.Format(SongSheetURL, Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = 10 * (int)jobject["data"]["num_pages"];

            foreach (var jToken in jobject["data"]["result"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                    MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
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
               .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
               .AddNode(string.Format(PlayListURL, Input.Id))
               .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            var Info = jobject["data"]["menusRespones"];
            Result.ListenNum = (string)Info["playNum"];
            Result.MusicNum = (int)Info["songNum"];
            Result.CreateTime = SyncStatic.ConvertStamptime((((long)Info["ctime"])/1000).ToString()).ToFmtDate(-1,"yyyy-MM-dd");
            Result.Logo= (string)Info["coverUrl"];
            Result.DissName = (string)Info["title"];
            foreach (var jToken in jobject["data"]["songsList"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = (long)jToken["id"],
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
               .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
               .AddNode(string.Format(PlayListURL, Input.AlbumId))
               .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            var Info = jobject["data"]["menusRespones"];
            Result.AlbumName = (string)Info["title"];
            foreach (var jToken in jobject["data"]["songsList"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = (long)jToken["id"],
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
              .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
              .AddNode((string)string.Format(PlayURL, Input.Dynamic))
              .Build(action:handle => {
                  handle.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
              })
              .RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.CanPlay = !jobject["data"]["cdns"][0].ToString().IsNullOrEmpty();
            Result.SongURL = jobject["data"]["cdns"][0].ToString();
            return Result;
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            throw new NotImplementedException();
        }
    }
}
