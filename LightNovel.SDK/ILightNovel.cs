using LightNovel.SDK.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK
{
    public interface ILightNovel
    {
        LightNovelResponseOutput LightNovelInit(LightNovelRequestInput Input, Action<ILightNovelCookie> action);
        LightNovelResponseOutput LightNovelSearch(LightNovelRequestInput Input, Action<ILightNovelCookie> action);
        LightNovelResponseOutput LightNovelCategory(LightNovelRequestInput Input, Action<ILightNovelCookie> action);
        LightNovelResponseOutput LightNovelDetail(LightNovelRequestInput Input, Action<ILightNovelCookie> action);
        LightNovelResponseOutput LightNovelView(LightNovelRequestInput Input, Action<ILightNovelCookie> action);
        LightNovelResponseOutput LightNovelContent(LightNovelRequestInput Input, Action<ILightNovelCookie> action);
    }
}
