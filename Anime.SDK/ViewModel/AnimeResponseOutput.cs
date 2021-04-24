using Anime.SDK.ViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.SDK.ViewModel
{
    public class AnimeResponseOutput
    {
        /// <summary>
        /// 播放地址
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PlayURL { get; set; }
        /// <summary>
        /// 推荐
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> RecommendCategory { get; set; }
        /// <summary>
        /// 每日更新
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AnimeWeekDayResult> WeekDays { get; set; }
        /// <summary>
        /// 搜索结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AnimeSearchResult SeachResults { get; set; }
        /// <summary>
        /// 详情结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<AnimeDetailResult> DetailResults { get; set; }
    }
}
