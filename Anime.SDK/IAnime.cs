using Anime.SDK.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK
{
    public interface IAnime
    {
        AnimeResponseOutput AnimeInit(AnimeRequestInput Input);
        AnimeResponseOutput AnimeSearch(AnimeRequestInput Input);
        AnimeResponseOutput AnimeCategory(AnimeRequestInput Input);
        AnimeResponseOutput AnimeCategoryType(AnimeRequestInput Input);
        AnimeResponseOutput AnimeDetail(AnimeRequestInput Input);
        AnimeResponseOutput AnimeWatchPlay(AnimeRequestInput Input);
    }
}
