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
        public static LightNovelFactory Novel(Action<LightNovelFactory> action)
        {
            LightNovelFactory factory = new LightNovelFactory();
            action(factory);
            if (factory.RequestParam == null)
                throw new NullReferenceException("RequestParam Is Null");
            return factory;
        }
        public LightNovelResponseOutput Runs()
        {
            ILightNovel light = new LightNovel();
            return RequestParam.LightNovelType switch
            {
                LightNovelEnum.Init => light.LightNovelInit(RequestParam),
                LightNovelEnum.Search => light.LightNovelInit(RequestParam),
                LightNovelEnum.Category => light.LightNovelInit(RequestParam),
                LightNovelEnum.Detail => light.LightNovelInit(RequestParam),
                LightNovelEnum.View => light.LightNovelInit(RequestParam),
                _ => light.LightNovelInit(RequestParam),
            };
        }
    }
}
