using HtmlAgilityPack;
using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using XExten.Advance.CacheFramework;
using XExten.Advance.HttpFramework.MultiCommon;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;

namespace Music.SDK.Basic.Impl
{
    internal class MiGuMusic : BasicMusic
    {
        private const string SongURL = "https://m.music.migu.cn/migu/remoting/scr_search_tag?rows=10&type={0}&keyword={1}&pgc={2}";
        private const string SongSheetURL = "https://m.music.migu.cn/migu/remoting/query_playlist_by_id_tag?onLine=1&queryChannel=0&createUserId=migu&contentCountMin=5&playListId={0}";
        private const string PlayListURL = "https://music.migu.cn/v3/music/playlist/{0}?page={1}";
        private const string AlbumURL = "https://m.music.migu.cn/migu/remoting/cms_album_song_list_tag?albumId={0}&pageSize=100";
        private const string PlayURL = "https://m.music.migu.cn/migu/remoting/cms_detail_tag?cpid={0}";
        private Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            {"Referer","http://m.music.migu.cn" },
            { "UserAgent","Mozilla/5.0"}
        };

        internal override MusicSongItemResult SearchSong(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                MusicPlatformType = MusicPlatformEnum.MiGuMusic,
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti
              .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
              .Header(Headers)
              .AddNode(string.Format(SongURL, 2, Input.KeyWord, Input.Page))
              .Build().RunString().FirstOrDefault();
            var jobject = response.ToModel<JObject>();
            Result.Total = (int)jobject["pgt"];
            foreach (var jToken in jobject["musics"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = jToken["copyrightId"].ToString(),
                    SongName = jToken["songName"].ToString(),
                    SongAlbumId = jToken["albumId"].ToString(),
                    SongAlbumName = jToken["albumName"].ToString(),
                };
                var SingerId = ((string)jToken["singerId"]).Split(new string[] { "," }, StringSplitOptions.None);
                var SingerName = ((string)jToken["singerName"]).Split(new string[] { "," }, StringSplitOptions.None);
                SongItem.SongArtistId.AddRange(SingerId);
                SongItem.SongArtistName.AddRange(SingerName);
                Result.SongItems.Add(SongItem);
            }
            return Result;
        }

        internal override MusicSongSheetResult SearchSongSheet(MusicSearch Input, MusicProxy Proxy)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                MusicPlatformType = MusicPlatformEnum.MiGuMusic,
                SongSheetItems = new List<MusicSongSheetItem>()
            };
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers)
                .AddNode(string.Format(SongURL, 6, Input.KeyWord, Input.Page))
                .Build().RunString().FirstOrDefault();
            var jobject = response.ToModel<JObject>();
            Result.Total = (int)jobject["pgt"];
            foreach (var jToken in jobject["songLists"])
            {
                MusicSongSheetItem SongSheetItem = new MusicSongSheetItem
                {
                    Cover = jToken["img"].ToString(),
                    SongSheetId = (long)jToken["id"],
                    ListenNumber = jToken["playNum"].ToString(),
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
                MusicPlatformType = MusicPlatformEnum.MiGuMusic,
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers)
                .AddNode(string.Format(SongSheetURL, Input.Id))
                .Build().RunString().FirstOrDefault();
            var jobject = response.ToModel<JObject>();
            HtmlWeb html = new HtmlWeb();
            HtmlDocument document = html.Load(string.Format(PlayListURL, Input.Id, Input.Page));
            var htmlNode = document.DocumentNode;
            Result.DissName = jobject["rsp"]["playList"][0]["playListName"].ToString();
            Result.Logo = jobject["rsp"]["playList"][0]["image"].ToString();
            Result.MusicNum = (int)jobject["rsp"]["playList"][0]["contentCount"];
            Result.CreateTime = ((DateTime)jobject["rsp"]["playList"][0]["createTime"]).ToString("yyyy-MM-dd");
            Result.ListenNum = htmlNode.SelectSingleNode("//div[@class='playcount']").InnerText.Split("：").LastOrDefault().Trim();
            foreach (var item in htmlNode.SelectNodes("//div[@class='row J_CopySong']"))
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongName = item.SelectSingleNode("div[@class='song-name J_SongName']/a").InnerText,
                    SongId = item.GetAttributeValue("data-cid", ""),
                    SongArtistName = item.SelectSingleNode("div[@class='song-singers J_SongSingers']/a").InnerText.Split(new string[] { "," }, StringSplitOptions.None).ToList(),
                    SongArtistId = item.GetAttributeValue("data-mid", "").Split(new string[] { "," }, StringSplitOptions.None).ToList(),
                    SongAlbumName = item.SelectSingleNode("div[@class='song-belongs']/a").GetAttributeValue("title", ""),
                    SongAlbumId = item.GetAttributeValue("data-aid", "")
                };
                Result.SongItems.Add(SongItem);
            }
            return Result;
        }

        internal override MusicSongAlbumDetailResult SongAlbumDetail(MusicAlbumSearch Input, MusicProxy Proxy)
        {
            MusicSongAlbumDetailResult Result = new MusicSongAlbumDetailResult
            {
                MusicPlatformType = MusicPlatformEnum.MiGuMusic,
                SongItems = new List<MusicSongItem>()
            };
            HtmlWeb html = new HtmlWeb();
            var htmlnode = html.Load($"http://music.migu.cn/v3/music/album/{Input.AlbumId}").DocumentNode;
            Result.AlbumName = htmlnode.SelectSingleNode("//h1[@class='title']").InnerText.Trim();
            var response = IHttpMultiClient.HttpMulti
                .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
                .Header(Headers).AddNode(string.Format(AlbumURL, Input.AlbumId))
                .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            foreach (var jToken in jobject["result"]["results"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = jToken["copyrightId"].ToString(),
                    SongName = jToken["songName"].ToString(),
                    SongAlbumId = Input.AlbumId,
                    SongAlbumName = Result.AlbumName,
                };
                var SingerId = jToken["singerId"].Select(t=>t.ToString());
                var SingerName = jToken["singerName"].Select(t => t.ToString());
                SongItem.SongArtistId.AddRange(SingerId);
                SongItem.SongArtistName.AddRange(SingerName);
                Result.SongItems.Add(SongItem);
            }

            return Result;
        }

        internal override MusicSongPlayAddressResult SongPlayAddress(MusicPlaySearch Input, MusicProxy Proxy)
        {
            MusicSongPlayAddressResult Result = new MusicSongPlayAddressResult
            {
                MusicPlatformType = MusicPlatformEnum.MiGuMusic
            };
            var response = IHttpMultiClient.HttpMulti
              .InitWebProxy((Proxy ?? new MusicProxy()).ToMapper<ProxyURL>())
              .Header(Headers).AddNode((string)string.Format(PlayURL, Input.Dynamic))
              .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();

            Result.CanPlay = !jobject["data"]["lisQq"].ToString().IsNullOrEmpty();
            Result.SongURL = HttpUtility.UrlDecode(jobject["data"]["lisQq"].ToString());
            //歌词
            if (Caches.RunTimeCacheGet<string>((string)Input.Dynamic).IsNullOrEmpty()) 
                Caches.RunTimeCacheSet<string>(Input.Dynamic, jobject["data"]["lyricLrc"].ToString(), 10);
            return Result;
        }

        internal override MusicLyricResult SongLyric(MusicLyricSearch Input, MusicProxy Proxy)
        {
            var Result = Caches.RunTimeCacheGet<string>((string)Input.Dynamic);
            if (Result.IsNullOrEmpty())
                return new MusicLyricResult { Title = "请先调用获取播放地址" };
            else
            {
                return new MusicLyricResult(Result);
            }
        }
    }
}
