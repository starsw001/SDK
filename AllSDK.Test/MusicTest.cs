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
        public static void MusicAllTest()
        {
            /* var SongItem = MusicFactory.Music(opt =>
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
             Console.WriteLine(SongItem.ToJson());*/
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
            var SongURL = MusicFactory.Music(opt =>
            {
                opt.RequestParam = new MusicRequestInput
                {
                    MusicPlatformType = MusicPlatformEnum.QQMusic,
                    MusicType = MusicTypeEnum.PlayAddress,
                    AddressSearch = new  MusicPlaySearch
                    {
                       Dynamic= SheetDetail.SongSheetDetailResult.SongItems.FirstOrDefault().SongMId
                    }
                };
            }).Runs();
            Console.WriteLine(SongURL.ToJson());
        }
    }
}
