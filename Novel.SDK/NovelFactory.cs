using Novel.SDK.ViewModel;
using Novel.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK
{
    public class NovelFactory
    {
        public NovelRequestInput RequestParam { get; set; }
        public static NovelFactory Novel(Action<NovelFactory> action) 
        {
            NovelFactory factory = new NovelFactory();
            action(factory);
            if (factory.RequestParam == null)
                throw new NullReferenceException("RequestParam Is Null");
            return factory;
        }
        public NovelResponseOutput Runs()
        {
            INovel novel = new Novel();
            return RequestParam.NovelType switch
            {
                NovelEnum.Init => novel.NovelInit(RequestParam),
                NovelEnum.Search => novel.NovelSearch(RequestParam),
                NovelEnum.Category => novel.NovelCategory(RequestParam),
                NovelEnum.Detail => novel.NovelDetail(RequestParam),
                NovelEnum.Watch => novel.NovelView(RequestParam),
                _ => null
            };
        }
    }
}
