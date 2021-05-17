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
    internal class BiliBiliMusic : BasicMusic
    {
        //search_type  = music (音乐) | menus (歌单) | musician (音乐家)
        private const string SongURL = "https://api.bilibili.com/audio/music-service-c/s?keyword={0}&page={1}&pagesize=10&search_type=music";
        private const string SongSheetURL = "";
        private const string PlayListURL = "https://api.bilibili.com/audio/music-service-c/menus/{0}";
        private const string AlbumURL = "";
        private const string PlayURL = "";
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
                    MusicPlatformType =  MusicPlatformEnum.BiliBiliMusic,
                    SongId = (long)jToken["id"],
                    SongName = (string)jToken["title"],
                    SongArtistName = new List<string>() { (string)jToken["up_name"] }
                };
                Result.SongItems.Add(SongItem);
            }

            return Result;
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
