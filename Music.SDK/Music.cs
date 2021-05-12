using Music.SDK.Basic;
using Music.SDK.Basic.Impl;
using Music.SDK.ViewModel;
using Music.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using XExten.Advance.StaticFramework;

namespace Music.SDK
{
    internal class Music : IMusic
    {
        private BasicMusic Instance(MusicPlatformEnum MusicPlatformType)
        {
            return MusicPlatformType switch
            {
                MusicPlatformEnum.QQMusic => new QQMusic(),
                MusicPlatformEnum.NeteaseMusic => new NeteaseMusic(),
                MusicPlatformEnum.KuGouMusic => new KuGouMusic(),
                MusicPlatformEnum.KuWoMusic => new KuWoMusic(),
                MusicPlatformEnum.BiliBiliMusic => new BiliBiliMusic(),
                MusicPlatformEnum.QianQianMusic => new QianQianMusic(),
                _ => throw new NullReferenceException(nameof(BasicMusic)),
            };
        }

        public MusicResponseOutput MusicSearchItem(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongItemResult = BaseMusic.SearchSong(Input.Search.KeyWord, Input.Search.Page);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicSearchSheet(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongSheetResult = BaseMusic.SearchSongSheet(Input.Search.KeyWord, Input.Search.Page);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicSearchSheetDetail(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongSheetDetailResult = BaseMusic.SongSheetDetail(Input.SheetSearch.Id);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicPlayAddress(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongPlayAddressResult = BaseMusic.SongPlayAddress(Input.AddressSearch.Dynamic);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicLyric(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongLyricResult = BaseMusic.SongLyric(Input.LyricSearch.Dynamic);
                return Result;
            }, ex => throw ex);
        }
    }
}
