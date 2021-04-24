using Newtonsoft.Json;
using Novel.SDK.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.SDK.ViewModel
{
    public class NovelResponseOutput
    {
        /// <summary>
        /// 推荐
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<NovelRecommendResult> IndexRecommends { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<NovelCategoryResult> IndexCategories { get; set; }
        /// <summary>
        /// 单个分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public NovelSingleCategoryResult SingleCategories { get; set; }
        /// <summary>
        /// 搜索结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<NovelSearchResult> SearchResults { get; set; }
        /// <summary>
        /// 详情结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public NovelDetailResult Details { get; set; }
        /// <summary>
        /// 内容结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public NovelContentResult Contents  { get; set; }
    }
}
