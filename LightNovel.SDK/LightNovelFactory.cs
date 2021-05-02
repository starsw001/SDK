using LightNovel.SDK.ViewModel;
using LightNovel.SDK.ViewModel.Enums;
using XExten.Advance.StaticFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK
{
    public class LightNovelFactory
    {
        public LightNovelRequestInput RequestParam { get; set; }
        public static LightNovelFactory LightNovel(Action<LightNovelFactory> action)
        {
            LightNovelFactory factory = new LightNovelFactory();
            action(factory);
            if (factory.RequestParam == null)
                throw new NullReferenceException("RequestParam Is Null");
            return factory;
        }
        public LightNovelResponseOutput Runs(Action<ILightNovelCookie> action=null)
        {
            ILightNovel light = new LightNovel();
            return RequestParam.LightNovelType switch
            {
                LightNovelEnum.Init => light.LightNovelInit(RequestParam, action ?? throw new ArgumentNullException(nameof(action))),
                LightNovelEnum.Search => light.LightNovelSearch(RequestParam, action ?? throw new ArgumentNullException(nameof(action))),
                LightNovelEnum.Category => light.LightNovelCategory(RequestParam, action ?? throw new ArgumentNullException(nameof(action))),
                LightNovelEnum.Detail => light.LightNovelDetail(RequestParam, action ?? throw new ArgumentNullException(nameof(action))),
                LightNovelEnum.View => light.LightNovelView(RequestParam, action ?? throw new ArgumentNullException(nameof(action))),
                LightNovelEnum.Content => light.LightNovelContent(RequestParam),
                _ => light.LightNovelInit(RequestParam, action ?? throw new ArgumentNullException(nameof(action))),
            };
        }
    }
}
