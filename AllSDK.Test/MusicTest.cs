using Music.SDK;
using Music.SDK.ViewModel;
using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XExten.Advance.LinqFramework;

namespace AllSDK.Test
{
    public class MusicTest
    {
        public static void MusicAllTest(int Type)
        {
            if (Type == 0)
            {
                #region QQ
                //单曲
                var SongItem = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.QQMusic,
                        MusicType = MusicTypeEnum.SongItem,
                        Search = new MusicSearch
                        {
                            KeyWord = "杨千嬅"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongItem.ToJson());
                Thread.Sleep(1000);
                //关联专辑
                var SongAlbum = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.QQMusic,
                        MusicType = MusicTypeEnum.AlbumDetail,
                        AlbumSearch = new MusicAlbumSearch
                        {
                            AlbumId = SongItem.SongItemResult.SongItems.FirstOrDefault().SongAlbumMId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongAlbum.ToJson());
                Thread.Sleep(1000);
                //歌单
                var SongSheet = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.QQMusic,
                        MusicType = MusicTypeEnum.SongSheet,
                        Search = new MusicSearch
                        {
                            KeyWord = "杨千嬅"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongSheet.ToJson());
                Thread.Sleep(1000);
                //歌单详情
                var SheetDetail = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.QQMusic,
                        MusicType = MusicTypeEnum.SheetDetail,
                        SheetSearch = new MusicSheetSearch
                        {
                            Id = SongSheet.SongSheetResult.SongSheetItems.FirstOrDefault().SongSheetId.AsString()
                        }
                    };
                }).Runs();
                Console.WriteLine(SheetDetail.ToJson());
                Thread.Sleep(1000);
                //地址
                var SongURL = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.QQMusic,
                        MusicType = MusicTypeEnum.PlayAddress,
                        AddressSearch = new MusicPlaySearch
                        {
                            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongMId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongURL.ToJson());
                Thread.Sleep(1000);
                //歌词
                var SongLyric = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.QQMusic,
                        MusicType = MusicTypeEnum.Lyric,
                        LyricSearch = new MusicLyricSearch
                        {
                            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongMId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongLyric.ToJson());
                #endregion
            }
            else if (Type == 1)
            {
                #region KuGou
                //单曲
                var SongItem = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                        MusicType = MusicTypeEnum.SongItem,
                        Search = new MusicSearch
                        {
                            KeyWord = "醉酒的蝴蝶"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongItem.ToJson());
                Thread.Sleep(1000);
                //歌单
                var SongSheet = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                        MusicType = MusicTypeEnum.SongSheet,
                        Search = new MusicSearch
                        {
                            KeyWord = "动漫"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongSheet.ToJson());
                Thread.Sleep(1000);
                //歌单详情
                var SheetDetail = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                        MusicType = MusicTypeEnum.SheetDetail,
                        SheetSearch = new MusicSheetSearch
                        {
                            Id = SongSheet.SongSheetResult.SongSheetItems.FirstOrDefault().SongSheetId.AsString()
                        }
                    };
                }).Runs();
                Console.WriteLine(SheetDetail.ToJson());
                Thread.Sleep(1000);
                //关联专辑
                var SongAlbum = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                        MusicType = MusicTypeEnum.AlbumDetail,
                        AlbumSearch = new MusicAlbumSearch
                        {
                            AlbumId = SongItem.SongItemResult.SongItems.LastOrDefault().SongAlbumId.AsString()
                        }
                    };
                }).Runs();
                Console.WriteLine(SongAlbum.ToJson());
                Thread.Sleep(1000);
                //地址
                var SongURL = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                        MusicType = MusicTypeEnum.PlayAddress,
                        AddressSearch = new MusicPlaySearch
                        {
                            KuGouAlbumId = SongItem.SongItemResult.SongItems.FirstOrDefault().SongAlbumId,
                            Dynamic = SongItem.SongItemResult.SongItems.FirstOrDefault().SongFileHash
                        }
                    };
                }).Runs();
                Console.WriteLine(SongURL.ToJson());
                Thread.Sleep(1000);
                //歌词
                var SongLyric = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuGouMusic,
                        MusicType = MusicTypeEnum.Lyric,
                        LyricSearch = new MusicLyricSearch
                        {
                            Dynamic = SongItem.SongItemResult.SongItems.FirstOrDefault().SongFileHash
                        }
                    };
                }).Runs();
                Console.WriteLine(SongLyric.ToJson());
                #endregion
            }
            else if (Type == 2)
            {
                #region KuWo
                //单曲
                var SongItem = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                        MusicType = MusicTypeEnum.SongItem,
                        Search = new MusicSearch
                        {
                            KeyWord = "处处吻"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongItem.ToJson());
                Thread.Sleep(1000);
                //歌单
                var SongSheet = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                        MusicType = MusicTypeEnum.SongSheet,
                        Search = new MusicSearch
                        {
                            KeyWord = "杨千嬅"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongSheet.ToJson());
                Thread.Sleep(1000);
                //歌单详情
                var SheetDetail = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                        MusicType = MusicTypeEnum.SheetDetail,
                        SheetSearch = new MusicSheetSearch
                        {
                            Id = SongSheet.SongSheetResult.SongSheetItems.FirstOrDefault().SongSheetId.AsString()
                        }
                    };
                }).Runs();
                Console.WriteLine(SheetDetail.ToJson());
                Thread.Sleep(1000);
                //关联专辑
                var SongAlbum = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                        MusicType = MusicTypeEnum.AlbumDetail,
                        AlbumSearch = new MusicAlbumSearch
                        {
                            AlbumId = SongItem.SongItemResult.SongItems.FirstOrDefault().SongAlbumId.AsString()
                        }
                    };
                }).Runs();
                Console.WriteLine(SongAlbum.ToJson());
                Thread.Sleep(1000);
                //地址
                var SongURL = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                        MusicType = MusicTypeEnum.PlayAddress,
                        AddressSearch = new MusicPlaySearch
                        {
                            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongURL.ToJson());
                Thread.Sleep(1000);
                //歌词
                var SongLyric = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                        MusicType = MusicTypeEnum.Lyric,
                        LyricSearch = new MusicLyricSearch
                        {
                            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongLyric.ToJson());
                
                #endregion
            }
            else if (Type == 3)
            {
                #region BiliBili
                //单曲
                var SongItem = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                        MusicType = MusicTypeEnum.SongItem,
                        Search = new MusicSearch
                        {
                            KeyWord = "醉酒的蝴蝶"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongItem.ToJson());
                Thread.Sleep(1000);
                //歌单
                var SongSheet = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                        MusicType = MusicTypeEnum.SongSheet,
                        Search = new MusicSearch
                        {
                            KeyWord = "起风了"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongSheet.ToJson());
                Thread.Sleep(1000);
                //歌单详情
                var SheetDetail = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                        MusicType = MusicTypeEnum.SheetDetail,
                        SheetSearch = new MusicSheetSearch
                        {
                            Id = SongSheet.SongSheetResult.SongSheetItems.FirstOrDefault().SongSheetId.AsString()
                        }
                    };
                }).Runs();
                Console.WriteLine(SheetDetail.ToJson());
                Thread.Sleep(1000);
                //地址
                var SongURL = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                        MusicType = MusicTypeEnum.PlayAddress,
                        AddressSearch = new MusicPlaySearch
                        {
                            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongURL.ToJson());
                Thread.Sleep(1000);
                //歌词
                var SongLyric = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                        MusicType = MusicTypeEnum.Lyric,
                        LyricSearch = new MusicLyricSearch
                        {
                            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongLyric.ToJson());
                #endregion
            }
            else if (Type == 4)
            {
                #region Netease
                //单曲
                //var SongItem = MusicFactory.Music(opt =>
                //{
                //    opt.RequestParam = new MusicRequestInput
                //    {
                //        MusicPlatformType = MusicPlatformEnum.NeteaseMusic,
                //        MusicType = MusicTypeEnum.SongItem,
                //        Search = new MusicSearch
                //        {
                //            KeyWord = "醉酒的蝴蝶"
                //        }
                //    };
                //}).Runs();
                //Console.WriteLine(SongItem.ToJson());
                //Thread.Sleep(1000);
                //歌单
                var SongSheet = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.NeteaseMusic,
                        MusicType = MusicTypeEnum.SongSheet,
                        Search = new MusicSearch
                        {
                            KeyWord = "自用gal"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongSheet.ToJson());
                Thread.Sleep(1000);
                //歌单详情
                var SheetDetail = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.NeteaseMusic,
                        MusicType = MusicTypeEnum.SheetDetail,
                        SheetSearch = new MusicSheetSearch
                        {
                            Id = SongSheet.SongSheetResult.SongSheetItems[2].SongSheetId.AsString()
                        }
                    };
                }).Runs();
                Console.WriteLine(SheetDetail.ToJson());
                Thread.Sleep(1000);
                //地址
                //var SongURL = MusicFactory.Music(opt =>
                //{
                //    opt.RequestParam = new MusicRequestInput
                //    {
                //        MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                //        MusicType = MusicTypeEnum.PlayAddress,
                //        AddressSearch = new MusicPlaySearch
                //        {
                //            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongId
                //        }
                //    };
                //}).Runs();
                //Console.WriteLine(SongURL.ToJson());
                //Thread.Sleep(1000);
                //歌词
                //var SongLyric = MusicFactory.Music(opt =>
                //{
                //    opt.RequestParam = new MusicRequestInput
                //    {
                //        MusicPlatformType = MusicPlatformEnum.BiliBiliMusic,
                //        MusicType = MusicTypeEnum.Lyric,
                //        LyricSearch = new MusicLyricSearch
                //        {
                //            Dynamic = SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongId
                //        }
                //    };
                //}).Runs();
                //Console.WriteLine(SongLyric.ToJson());
                #endregion
            }
        }
    }
}
