using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel
{
    /// <summary>
    /// 每日推荐
    /// </summary>
    public class AnimeWeekDay
    {
        public string DayName { get; set; }
        public List<AnimeWeekDayRecommend> DayRecommends { get; set; }
    }
    /// <summary>
    /// 每日推荐
    /// </summary>
    public class AnimeWeekDayRecommend
    {
        public string AnimeName { get; set; }
        public string AnimeURL { get; set; }
    }
}
