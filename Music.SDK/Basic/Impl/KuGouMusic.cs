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
        private const string SongURL = "https://songsearch.kugou.com/song_search_v2?keyword={0}&page={1}";
        private Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            {"Referer","https://www.kugou.com/" },
            {"Origin","https://www.kugou.com/" },
            {"Cookie","kg_mid=c4ca4238a0b923820dcc509a6f75849b;"}
        };

        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers).AddNode(string.Format(SongURL, Input.KeyWord, Input.Page))
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
