using Music.SDK;
using Music.SDK.ViewModel;
using Music.SDK.ViewModel.Enums;
using Music.SDK.ViewModel.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                            KeyWord = "处处吻"
                        }
                    };
                }).Runs();
                Console.WriteLine(SongItem.ToJson());
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
                //地址
                var SongURL = MusicFactory.Music(opt =>
                {
                    opt.RequestParam = new MusicRequestInput
                    {
                        MusicPlatformType = MusicPlatformEnum.KuWoMusic,
                        MusicType = MusicTypeEnum.PlayAddress,
                        AddressSearch = new MusicPlaySearch
                        {
                            Dynamic = 89656617//SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongId
                        }
                    };
                }).Runs();
                Console.WriteLine(SongURL.ToJson());
                #endregion
            }
        }
    }
}
