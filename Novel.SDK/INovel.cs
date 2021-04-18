using Novel.SDK.ViewModel;
using System;

namespace Novel.SDK
{
    public interface INovel
    {
        NovelResponseOutput NovelInit(NovelRequestInput Input);

        NovelResponseOutput NovelSearch(NovelRequestInput Input);

        NovelResponseOutput NovelCategory(NovelRequestInput Input);

        NovelResponseOutput NovelDetail(NovelRequestInput Input);

        NovelResponseOutput NovelView(NovelRequestInput Input);
    }
}
