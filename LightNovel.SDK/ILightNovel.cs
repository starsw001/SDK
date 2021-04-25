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
        LightNovelResponseOutput LightNovelInit(LightNovelRequestInput Input);
        LightNovelResponseOutput LightNovelSearch(LightNovelRequestInput Input);
        LightNovelResponseOutput LightNovelCategory(LightNovelRequestInput Input);
        LightNovelResponseOutput LightNovelDetail(LightNovelRequestInput Input);
        LightNovelResponseOutput LightNovelView(LightNovelRequestInput Input);
    }
}
