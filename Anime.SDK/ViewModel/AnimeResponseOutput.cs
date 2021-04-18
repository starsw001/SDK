using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel
{
    public class AnimeResponseOutput
    {
        public string PlayURL { get; set; }
        public Dictionary<string, string> RecommendCategory { get; set; }
        public List<AnimeWeekDay> WeekDays { get; set; }
        public AnimeSearchResult SeachResults { get; set; }
        public List<AnimeDetailResult> DetailResults { get; set; }
    }
}
