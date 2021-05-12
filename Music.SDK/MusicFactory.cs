using Music.SDK.ViewModel;
using Music.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Music.SDK
{
    public class MusicFactory
    {
        public MusicRequestInput RequestParam { get; set; }
        public static MusicFactory Music(Action<MusicFactory> action)
        {
            MusicFactory factory = new MusicFactory();
            action(factory);
            if (factory.RequestParam == null)
                throw new NullReferenceException("RequestParam Is Null");
            return factory;
        }
        public MusicResponseOutput Runs()
        {
            IMusic music = new Music();
            return RequestParam.MusicType switch
            {
                MusicTypeEnum.SongItem => music.MusicSearchItem(RequestParam),
                MusicTypeEnum.SongSheet => music.MusicSearchSheet(RequestParam),
                MusicTypeEnum.SheetDetail=> music.MusicSearchSheetDetail(RequestParam),
                _ => null
            };
        }
    }
}
