using Novel.SDK;
using Synctool.LinqFramework;
using System;

namespace SDKTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var Init = NovelFactory.Novel(item =>
            // {
            //     item.RequestParam = new Novel.SDK.ViewModel.NovelRequestInput
            //     {
            //         NovelType = Novel.SDK.ViewModel.NovelEnum.Init
            //     };
            // }).Runs();
            var Search = NovelFactory.Novel(item =>
            {
                item.RequestParam = new Novel.SDK.ViewModel.NovelRequestInput
                {
                    NovelType = Novel.SDK.ViewModel.NovelEnum.Search,
                    NovelSearchKeyWord="校"
                };
            }).Runs();
            //var Cate = NovelFactory.Novel(item =>
            //{
            //    item.RequestParam = new Novel.SDK.ViewModel.NovelRequestInput
            //    {
            //        NovelType = Novel.SDK.ViewModel.NovelEnum.Category,
            //        NovelCategoryAddress =Init.IndexCategories[0].CollectAddress
            //    };
            //}).Runs();
            Console.WriteLine(Search.ToJson());
        }
    }
}
