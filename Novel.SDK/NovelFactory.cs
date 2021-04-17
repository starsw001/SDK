using Novel.SDK.ViewModel;
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
                NovelEnum.Search => novel.NovelInit(RequestParam),
                NovelEnum.Category => novel.NovelInit(RequestParam),
                NovelEnum.Detail => novel.NovelInit(RequestParam),
                NovelEnum.Watch => novel.NovelInit(RequestParam),
                _ => novel.NovelInit(RequestParam),
            };
        }
    }
}
