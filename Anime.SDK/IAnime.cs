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
        AnimeResponseOutput AnimeInit(AnimeRequstInput Input);
        AnimeResponseOutput AnimeSearch(AnimeRequstInput Input);
        AnimeResponseOutput AnimeCategory(AnimeRequstInput Input);
        AnimeResponseOutput AnimeDetail(AnimeRequstInput Input);
        AnimeResponseOutput AnimeWatchPlay(AnimeRequstInput Input);
    }
}
