using LightNovel.SDK.ViewModel;
using LightNovel.SDK.ViewModel.Enums;
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
        public LightNovelResponseOutput Runs(Action<ILightNovelCookie> action)
        {
            ILightNovel light = new LightNovel();
            return RequestParam.LightNovelType switch
            {
                LightNovelEnum.Init => light.LightNovelInit(RequestParam, action),
                LightNovelEnum.Search => light.LightNovelSearch(RequestParam, action),
                LightNovelEnum.Category => light.LightNovelCategory(RequestParam, action),
                LightNovelEnum.Detail => light.LightNovelDetail(RequestParam, action),
                LightNovelEnum.View => light.LightNovelView(RequestParam, action),
                _ => light.LightNovelInit(RequestParam, action),
            };
        }
    }
}
