using Novel.SDK.ViewModel;
using System;

namespace Novel.SDK
{
    public interface INovel
    {
        NovelResponseOutput NovelInit(NovelRequestInput Input);
    }
}
