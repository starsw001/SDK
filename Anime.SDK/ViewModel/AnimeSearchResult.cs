using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel
{
    public class AnimeSearchResult
    {
        public int Page { get; set; }
        public List<AnimeSearchResults> Searchs { get; set; }
    }
    public class AnimeSearchResults
    {
        public string DetailAddress { get; set; }
        public string AnimeCover { get; set; }
        public string AnimeName { get; set; }
    }
}
