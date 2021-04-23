using Newtonsoft.Json;
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
        public List<NovelRecommend> IndexRecommends { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<NovelCategory> IndexCategories { get; set; }
        /// <summary>
        /// 单个分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public NovelSingleCategory SingleCategories { get; set; }
        /// <summary>
        /// 搜索结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<NovelSearch> SearchResults { get; set; }
        /// <summary>
        /// 详情结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public NovelDetail Details { get; set; }
        /// <summary>
        /// 内容结果
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public NovelContent Contents  { get; set; }
    }
}
