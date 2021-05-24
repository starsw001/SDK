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
                MusicPlatformEnum.MiGuMusic => new MiGuMusic(),
                _ => throw new NullReferenceException(nameof(BasicMusic)),
            };
        }

        public MusicResponseOutput MusicSearchItem(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongItemResult = BaseMusic.SearchSong(Input.Search,Input.Proxy);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicSearchSheet(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongSheetResult = BaseMusic.SearchSongSheet(Input.Search, Input.Proxy);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicSearchAlbumDetail(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongAlbumDetailResult = BaseMusic.SongAlbumDetail(Input.AlbumSearch, Input.Proxy);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicSearchSheetDetail(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongSheetDetailResult = BaseMusic.SongSheetDetail(Input.SheetSearch, Input.Proxy);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicPlayAddress(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongPlayAddressResult = BaseMusic.SongPlayAddress(Input.AddressSearch, Input.Proxy);
                return Result;
            }, ex => throw ex);
        }

        public MusicResponseOutput MusicLyric(MusicRequestInput Input)
        {
            return SyncStatic.TryCatch(() =>
            {
                BasicMusic BaseMusic = Instance(Input.MusicPlatformType);
                MusicResponseOutput Result = new MusicResponseOutput();
                Result.SongLyricResult = BaseMusic.SongLyric(Input.LyricSearch, Input.Proxy);
                return Result;
            }, ex => throw ex);
        }

    }
}
