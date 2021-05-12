using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Response;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using XExten.Advance.HttpFramework.MultiFactory;
using XExten.Advance.LinqFramework;
using XExten.Advance.StaticFramework;

namespace Music.SDK.Basic.Impl
{
    internal class QQMusic : BasicMusic
    {
        private const string SongURL = "https://i.y.qq.com/s.music/fcgi-bin/search_for_qq_cp?g_tk=938407465&uin=0&format=json&inCharset=utf-8&outCharset=utf-8&notice=0&platform=h5&needNewCode=1&w={0}&zhidaqu=1&catZhida=1&t=0&flag=1&ie=utf-8&sem=1&aggr=0&perpage=20&n=20&p={1}&remoteplace=txt.mqq.all&jsonpCallback=json";
        private const string SongSheetURL = "https://c.y.qq.com/soso/fcgi-bin/client_music_search_songlist?remoteplace=txt.yqq.top&searchid=1&flag_qc=0&page_no={0}&num_per_page=20&query={1}&cv=4747474&ct=24&format=json&inCharset=utf-8&outCharset=utf-8&notice=0&platform=yqq.json&needNewCode=0&uin=0&hostUin=0&loginUin=0";
        private const string PlayListURL = "https://c.y.qq.com/qzone/fcg-bin/fcg_ucc_getcdinfo_byids_cp.fcg?type=1&json=1&utf8=1&onlysong=0&new_format=1&disstid={0}&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq.json&needNewCode=0";
        private const string PlayURL = "https://u.y.qq.com/cgi-bin/musicu.fcg?data%3D%7B%22req%22%3A%7B%22module%22%3A%22CDN.SrfCdnDispatchServer%22%2C%22method%22%3A%22GetCdnDispatch%22%2C%22param%22%3A%7B%22guid%22%3A%221535153710%22%2C%22calltype%22%3A0%2C%22userip%22%3A%22%22%7D%7D%2C%22req_0%22%3A%7B%22module%22%3A%22vkey.GetVkeyServer%22%2C%22method%22%3A%22CgiGetVkey%22%2C%22param%22%3A%7B%22guid%22%3A%221535153710%22%2C%22songmid%22%3A%5B%22{0}%22%5D%2C%22songtype%22%3A%5B0%5D%2C%22uin%22%3A%220%22%2C%22loginflag%22%3A1%2C%22platform%22%3A%2220%22%7D%7D%2C%22comm%22%3A%7B%22uin%22%3A0%2C%22format%22%3A%22json%22%2C%22ct%22%3A24%2C%22cv%22%3A0%7D%7D";
        private const string LyricURL = "https://i.y.qq.com/lyric/fcgi-bin/fcg_query_lyric.fcg?songmid={0}&loginUin=0&hostUin=0&format=jsonp&inCharset=GB2312&outCharset=utf-8&notice=0&platform=yqq&jsonpCallback=MusicJsonCallback&needNewCode=0";
        private Dictionary<string, string> Headers = new Dictionary<string, string>
        {
            {"Referer","https://y.qq.com" },
            {"Origin","https://y.qq.com" }
        };

        internal override MusicSongItemResult SearchSong(string KeyWord, int Page)
        {
            MusicSongItemResult Result = new MusicSongItemResult
            {
                SongItems = new List<MusicSongItem>()
            };
            var response = IHttpMultiClient.HttpMulti.Headers(Headers)
                 .AddNode(string.Format(SongURL, KeyWord, Page))
                 .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.Total = jobject.SelectToken("data.song.totalnum").ToString().AsInt();
            foreach (var jToken in jobject["data"]["song"]["list"])
            {
                MusicSongItem Item = new MusicSongItem
                {
                    MusicPlatformType = MusicPlatformEnum.QQMusic,
                    SongUrl = (string)jToken["songurl"],
                    SongId = (long)jToken["songid"],
                    SongMId = (string)jToken["songmid"],
                    SongGId = (string)jToken["songmid"],
                    SongName = (string)jToken["songname"],
                    SongAlbumId = (long)jToken["albumid"],
                    SongAlbumMId = (string)jToken["alubmmid"],
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
        internal override MusicSongSheetResult SearchSongSheet(string KeyWord, int Page)
        {
            MusicSongSheetResult Result = new MusicSongSheetResult
            {
                SongSheetItems = new List<MusicSongSheetItem>()
            };
            var response = IHttpMultiClient.HttpMulti.Headers(Headers)
                .AddNode(string.Format(SongSheetURL, Page,KeyWord))
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
        internal override MusicSongSheetDetailResult SongSheetDetail(string SheetId)
        {
            MusicSongSheetDetailResult Result = new MusicSongSheetDetailResult
            {
                SongItems = new List<MusicSongItem>()
            };

            var response = IHttpMultiClient.HttpMulti.Headers(Headers)
               .AddNode(string.Format(PlayListURL, SheetId))
               .Build().RunString().FirstOrDefault();

            var jobject = response.ToModel<JObject>();
            Result.MusicNum = (int)jobject["cdlist"][0]["total_song_num"];
            Result.CreateTime = SyncStatic.ConvertStamptime((string)jobject["cdlist"][0]["ctime"]).ToString("yyyy-MM-dd");
            Result.DissName = (string)jobject["cdlist"][0]["dissname"];
            Result.ListenNum = (string)jobject["cdlist"][0]["visitnum"];
            Result.Logo=(string)jobject["cdlist"][0]["logo"];
            Result.MusicPlatformType = MusicPlatformEnum.QQMusic;
            foreach (var jToken in jobject["cdlist"][0]["songlist"])
            {
                MusicSongItem SongItem = new MusicSongItem
                {
                    SongId = (long)jToken["id"],
                    SongMId = (string)jToken["mid"],
                    SongGId = (string)jToken["mid"],
                    SongName = (string)jToken["name"],
                    SongAlbumId = (long)jToken["album"]["id"],
                    SongAlbumMId = (string)jToken["album"]["mid"],
                    SongAlbumName = (string)jToken["album"]["name"]
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
    }
}
