using LightNovel.SDK.ViewModel.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightNovel.SDK.ViewModel
{
    public class LightNovelResponseOutput
    {
        /// <summary>
        /// 分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<LightNovelCategoryResult> CategoryResults { get; set; }
        /// <summary>
        /// 检索
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LightNovelSearchResults SearchResults { get; set; }
        /// <summary>
        /// 单个分类
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public LightNovelSingleCategoryResult SingleCategoryResult { get; set; }
    }
}
