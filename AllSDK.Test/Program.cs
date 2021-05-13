using System;

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
            //音乐SDK
            MusicTest.MusicAllTest(1);
            Console.WriteLine("自动化测试完成，按任意键退出");
            Console.ReadKey();
        }

    }
}
