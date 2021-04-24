using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel.Response
{
    /// <summary>
    /// 每日推荐
    /// </summary>
    public class AnimeWeekDayResult
    {
        public string DayName { get; set; }
        public List<AnimeWeekDayRecommendResult> DayRecommends { get; set; }
    }
    /// <summary>
    /// 每日推荐
    /// </summary>
    public class AnimeWeekDayRecommendResult
    {
        public string AnimeName { get; set; }
        public string AnimeURL { get; set; }
    }
}
