using Anime.SDK.ViewModel;
using Anime.SDK.ViewModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK
{
    public class AnimeFactory
    {
        public AnimeRequestInput RequestParam { get; set; }
        public static AnimeFactory Anime(Action<AnimeFactory> action)
        {
            AnimeFactory factory = new AnimeFactory();
            action(factory);
            if (factory.RequestParam == null)
                throw new NullReferenceException("RequestParam Is Null");
            return factory;
        }
        public AnimeResponseOutput Runs()
        {
            IAnime anime = new Anime();
            return RequestParam.AnimeType switch
            {
                AnimeEnum.Init => anime.AnimeInit(RequestParam),
                AnimeEnum.Search => anime.AnimeSearch(RequestParam),
                AnimeEnum.Category => anime.AnimeCategory(RequestParam),
                AnimeEnum.Detail => anime.AnimeDetail(RequestParam),
                AnimeEnum.Watch => anime.AnimeWatchPlay(RequestParam),
                _ => anime.AnimeInit(RequestParam),
            };
        }
    }
}
