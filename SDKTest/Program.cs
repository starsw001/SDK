using Novel.SDK;
using System;

namespace SDKTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NovelFactory.Novel(item =>
            {
                item.RequestParam = new Novel.SDK.ViewModel.NovelRequestInput
                {
                    NovelType = Novel.SDK.ViewModel.NovelEnum.Init
                };
            }).Runs();
        }
    }
}
