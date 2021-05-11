using Music.SDK.Basic;
using Music.SDK.Basic.Impl;
using System;
using XExten.Advance.LinqFramework;

namespace AllSDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始自动化测试...");
            //动漫SDK
            //AnimeTest.AnimeAllTest();
            //小说SDK
            //NovelTest.NovelAllTest();
            //轻小说SDK
            //LightNovelTest.LightNovelAllTest();
            //图片SDK
            //WallpaperTest.WallpaperAllTest();
            BasicMusic music = new QQMusic();
            Console.WriteLine(music.SearchSongSheet("galgame").ToJson());
            Console.WriteLine("自动化测试完成，按任意键退出");
            Console.ReadKey();
        }

    }
}
