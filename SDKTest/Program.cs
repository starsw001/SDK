using Novel.SDK;
using Synctool.LinqFramework;
using System;

namespace SDKTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Init = NovelFactory.Novel(item =>
             {
                 item.RequestParam = new Novel.SDK.ViewModel.NovelRequestInput
                 {
                     NovelType = Novel.SDK.ViewModel.NovelEnum.Init
                 };
             }).Runs();
            Console.WriteLine(Init.ToJson());
        }
    }
}
